using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;
using Garnet.Teams.Application.TeamUserJoinRequest.Notifications;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Commands
{
    public class TeamUserJoinRequestDecideCommand
    {
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly ITeamUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMessageBus _messageBus;

        public TeamUserJoinRequestDecideCommand(
            ICurrentUserProvider currentUserProvider,
            ITeamUserJoinRequestRepository userJoinRequestRepository,
            ITeamParticipantRepository participantRepository,
            ITeamUserRepository userRepository,
            ITeamRepository teamRepository,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _messageBus = messageBus;
            _teamRepository = teamRepository;
            _participantRepository = participantRepository;
            _userRepository = userRepository;
            _userJoinRequestRepository = userJoinRequestRepository;
        }

        public async Task<Result<TeamUserJoinRequestEntity>> Execute(CancellationToken ct, string userJoinRequestId, bool isApproved)
        {
            var userJoinRequest = await _userJoinRequestRepository.GetUserJoinRequestById(ct, userJoinRequestId);
            if (userJoinRequest is null)
            {
                return Result.Fail(new TeamUserJoinRequestNotFoundError(userJoinRequestId));
            }

            var team = await _teamRepository.GetTeamById(ct, userJoinRequest!.TeamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(userJoinRequest.TeamId));
            }

            if (team.OwnerUserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamUserJoinRequestOnlyOwnerCanDecideError());
            }

            var user = await _userRepository.GetUser(ct, userJoinRequest.UserId);

            if (isApproved & user is not null)
            {
                var args = new TeamParticipantCreateArgs(user!.Id, user.Username, user.AvatarUrl, team.Id);
                await _participantRepository.CreateTeamParticipant(ct, args);
                await _teamRepository.IncreaseParticipantCount(ct, team.Id, user.AvatarUrl);
            }
            await _userJoinRequestRepository.DeleteUserJoinRequestById(ct, userJoinRequestId);

            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(userJoinRequest.UserId));
            }

            var @event = userJoinRequest.ToDecidedEvent(isApproved);
            await _messageBus.Publish(@event);

            var notification = userJoinRequest.CreateTeamUserJoinRequestDecideNotification(team, isApproved);
            await _messageBus.Publish(notification);
            return Result.Ok(userJoinRequest);
        }
    }
}