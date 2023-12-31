using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Errors;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamEditTagsCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;
        private readonly ICurrentUserProvider _currentUserProvider;

        public TeamEditTagsCommand(
            ITeamRepository teamRepository,
            ICurrentUserProvider currentUserProvider,
            IMessageBus messageBus)
        {
            _teamRepository = teamRepository;
            _messageBus = messageBus;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<Result<TeamEntity>> Execute(CancellationToken ct, string teamId, string[] tags)
        {
            var userId = _currentUserProvider.UserId;

            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != userId)
            {
                return Result.Fail(new TeamOnlyOwnerCanChangeTagsError());
            }

            team = await _teamRepository.EditTeamTags(ct, teamId, tags);

            var @event = team!.ToUpdatedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(team!);
        }
    }
}