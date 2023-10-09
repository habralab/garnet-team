using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;
using Garnet.Teams.Application.TeamUserJoinRequest.Entities;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;
using Garnet.Teams.Events.TeamUserJoinRequest;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Commands
{
    public class TeamUserJoinRequestDecideCommand
    {
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly ITeamUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;

        public TeamUserJoinRequestDecideCommand(ITeamUserJoinRequestRepository userJoinRequestRepository, ITeamParticipantRepository participantRepository, ITeamUserRepository userRepository, ITeamRepository teamRepository, IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _teamRepository = teamRepository;
            _participantRepository = participantRepository;
            _userRepository = userRepository;
            _userJoinRequestRepository = userJoinRequestRepository;
        }

        public async Task<Result<TeamUserJoinRequestEntity>> Execute(CancellationToken ct, ICurrentUserProvider currentUserProvider, string userJoinRequestId, bool isApproved)
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

            if (team.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamUserJoinRequestOnlyOwnerCanDecideError());
            }

            var user = await _userRepository.GetUser(ct, userJoinRequest.UserId);

            if (isApproved & user is not null)
            {
                await _participantRepository.CreateTeamParticipant(ct, user!.Id, user.Username, userJoinRequest.TeamId);
            }
            await _userJoinRequestRepository.DeleteUserJoinRequestById(ct, userJoinRequestId);

            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(userJoinRequest.UserId));
            }

            var @event = new TeamUserJoinRequestDecidedEvent(userJoinRequest.Id, userJoinRequest.UserId, userJoinRequest.TeamId, isApproved);
            await _messageBus.Publish(@event);
            return Result.Ok(userJoinRequest);
        }
    }
}