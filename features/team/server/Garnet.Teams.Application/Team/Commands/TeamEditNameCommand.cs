using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;

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

        public Task<Result<TeamEntity>> Execute(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId, string name)
        {
            return null;
        }
    }
}