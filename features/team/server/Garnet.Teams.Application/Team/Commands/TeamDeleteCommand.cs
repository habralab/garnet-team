using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamJoinInvitation;
using Garnet.Teams.Application.TeamJoinProjectRequest;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamUserJoinRequest;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamDeleteCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly ITeamJoinInvitationRepository _joinInvitationRepository;
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;
        private readonly IMessageBus _messageBus;

        public TeamDeleteCommand(
            ITeamRepository teamRepository,
            ITeamParticipantRepository participantRepository,
            ITeamJoinInvitationRepository joinInvitationRepository,
            ITeamJoinProjectRequestRepository joinProjectRequestRepository,
            ITeamUserJoinRequestRepository userJoinRequestRepository,
            IMessageBus messageBus)
        {
            _teamRepository = teamRepository;
            _participantRepository = participantRepository;
            _joinInvitationRepository = joinInvitationRepository;
            _joinProjectRequestRepository = joinProjectRequestRepository;
            _userJoinRequestRepository = userJoinRequestRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamEntity>> Execute(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanDeleteError());
            }

            await _joinProjectRequestRepository.DeleteJoinProjectRequestByTeam(ct, teamId);
            await _joinInvitationRepository.DeleteInvitationsByTeam(ct, teamId);
            await _userJoinRequestRepository.DeleteUserJoinRequestsByTeam(ct, teamId);
            await _participantRepository.DeleteParticipantsByTeam(ct, teamId);
            await _teamRepository.DeleteTeam(ct, teamId);

            var @event = team.ToDeletedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(team);
        }
    }
}