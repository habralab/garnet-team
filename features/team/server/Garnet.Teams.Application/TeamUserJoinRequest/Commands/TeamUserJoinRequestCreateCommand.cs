using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamJoinInvitation;
using Garnet.Teams.Application.TeamJoinInvitation.Args;
using Garnet.Teams.Application.TeamJoinInvitation.Errors;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Errors;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Commands
{
    public class TeamUserJoinRequestCreateCommand
    {
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly ITeamUserRepository _userRepository;
        private readonly IMessageBus _messageBus;
        private readonly ITeamJoinInvitationRepository _teamJoinInvitationRepository;

        public TeamUserJoinRequestCreateCommand(
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            ITeamJoinInvitationRepository teamJoinInvitationRepository,
            ITeamUserJoinRequestRepository userJoinRequestRepository,
            ITeamParticipantRepository participantRepository,
            ITeamUserRepository userRepository,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _teamJoinInvitationRepository = teamJoinInvitationRepository;
            _participantRepository = participantRepository;
            _userJoinRequestRepository = userJoinRequestRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamUserJoinRequestEntity>> Execute(CancellationToken ct, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            var user = await _userRepository.GetUser(ct, _currentUserProvider.UserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(_currentUserProvider.UserId));
            }

            var teamUserJoinRequests = await _userJoinRequestRepository.GetAllUserJoinRequestsByTeam(ct, teamId);
            if (teamUserJoinRequests.Any(x => x.UserId == _currentUserProvider.UserId))
            {
                return Result.Fail(new TeamPendingUserJoinRequestError(_currentUserProvider.UserId));
            }

            var joinInvitationFilter = new TeamJoinInvitationFilterArgs(user.Id, teamId);
            var teamJoinInvitations = await _teamJoinInvitationRepository.FilterInvitations(ct, joinInvitationFilter);
            if (teamJoinInvitations.Length > 0)
            {
                return Result.Fail(new TeamPendingJoinInvitationError(user.Id));
            }

            var membership = await _participantRepository.IsParticipantInTeam(ct, user.Id, teamId);
            if (membership is not null)
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