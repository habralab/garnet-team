using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Errors;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;
using Garnet.Teams.Events.TeamUserJoinRequest;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Commands
{
    public class TeamUserJoinRequestCreateCommand
    {
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly ITeamUserRepository _userRepository;
        private readonly IMessageBus _messageBus;

        public TeamUserJoinRequestCreateCommand(
            ITeamRepository teamRepository,
            ITeamUserJoinRequestRepository userJoinRequestRepository,
            ITeamParticipantRepository participantRepository,
            ITeamUserRepository userRepository,
            IMessageBus messageBus)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _participantRepository = participantRepository;
            _userJoinRequestRepository = userJoinRequestRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamUserJoinRequestEntity>> Execute(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            var user = await _userRepository.GetUser(ct, currentUserProvider.UserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(currentUserProvider.UserId));
            }

            var teamUserJoinRequests = await _userJoinRequestRepository.GetAllUserJoinRequestsByTeam(ct, teamId);
            if (teamUserJoinRequests.Any(x => x.UserId == currentUserProvider.UserId))
            {
                return Result.Fail(new TeamPendingUserJoinRequestError(currentUserProvider.UserId));
            }

            var userTeams = await _participantRepository.GetMembershipOfUser(ct, user.Id);
            if (userTeams.Any(x=> x.TeamId == teamId))
            {
                return Result.Fail(new TeamUserIsAlreadyAParticipantError(user.Id));
            }

            var request = await _userJoinRequestRepository.CreateJoinRequestByUser(ct, user.Id, teamId);

            var @event = request.ToCreatedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(request);
        }
    }
}