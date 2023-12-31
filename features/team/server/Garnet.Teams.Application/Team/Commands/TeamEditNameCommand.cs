using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Errors;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamEditNameCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;
        public TeamEditNameCommand(
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _teamRepository = teamRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamEntity>> Execute(CancellationToken ct, string teamId, string name)
        {
            var userId = _currentUserProvider.UserId;
            
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name)) {
                return Result.Fail(new TeamNameCanNotBeEmptyError());
            }

            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != userId)
            {
                return Result.Fail(new TeamOnlyOwnerCanChangeName());
            }

            team = await _teamRepository.EditTeamName(ct, teamId, name);

            var @event = team!.ToUpdatedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(team!);
        }
    }
}