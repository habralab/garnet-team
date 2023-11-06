using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamProject;
using Garnet.Teams.Events.TeamJoinProjectRequest;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamLeaveProjectCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamProjectRepository _teamProjectRepository;
        private readonly IMessageBus _messageBus;
        public TeamLeaveProjectCommand(
            ITeamRepository teamRepository,
            ITeamProjectRepository teamProjectRepository,
            ICurrentUserProvider currentUserProvider,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _teamRepository = teamRepository;
            _messageBus = messageBus;
            _teamProjectRepository = teamProjectRepository;
        }

        public async Task<Result<string>> Execute(CancellationToken ct, string teamId, string projectId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team.OwnerUserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanDeleteFromProject());
            }

            await _teamProjectRepository.RemoveTeamProjectInTeam(ct, projectId, teamId);

            var @event = new TeamLeaveProjectEvent(teamId, projectId);
            await _messageBus.Publish(@event);
            return Result.Ok(teamId);
        }
    }
}