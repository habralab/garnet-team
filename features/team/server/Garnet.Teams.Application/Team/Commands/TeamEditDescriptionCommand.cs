using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Args;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Events.Team;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamEditDescriptionCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;


        public TeamEditDescriptionCommand(ITeamRepository teamRepository, IMessageBus messageBus)
        {
            _teamRepository = teamRepository;
            _messageBus = messageBus;
        }

         public async Task<Result<TeamEntity>> Execute(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId, string description)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanEditError());
            }

            team = await _teamRepository.EditTeamDescription(ct, teamId, description);

            var @event = new TeamUpdatedEvent(team!.Id, team.Name, team.OwnerUserId, team.Description, team.Tags);
            await _messageBus.Publish(@event);
            return Result.Ok(team!);
        }
    }
}