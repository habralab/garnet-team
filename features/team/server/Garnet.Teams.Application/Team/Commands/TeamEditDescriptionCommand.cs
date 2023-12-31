using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Errors;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamEditDescriptionCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;


        public TeamEditDescriptionCommand(ICurrentUserProvider currentUserProvider, ITeamRepository teamRepository, IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _teamRepository = teamRepository;
            _messageBus = messageBus;
        }

         public async Task<Result<TeamEntity>> Execute(CancellationToken ct, string teamId, string description)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanChangeDescriptionError());
            }

            team = await _teamRepository.EditTeamDescription(ct, teamId, description);

            var @event = team!.ToUpdatedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(team!);
        }
    }
}