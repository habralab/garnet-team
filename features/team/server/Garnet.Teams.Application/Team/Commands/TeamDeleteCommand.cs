using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Events.Team;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamDeleteCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly IMessageBus _messageBus;

        public TeamDeleteCommand(
            ITeamRepository teamRepository,
            ITeamParticipantRepository participantRepository,
            IMessageBus messageBus)
        {
            _teamRepository = teamRepository;
            _participantRepository = participantRepository;
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

            await _teamRepository.DeleteTeam(ct, teamId);
            await _participantRepository.DeleteTeamParticipants(ct, teamId);

            var @event = team.ToDeletedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(team);
        }
    }
}