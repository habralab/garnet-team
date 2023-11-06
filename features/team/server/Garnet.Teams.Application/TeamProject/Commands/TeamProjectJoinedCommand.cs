using FluentResults;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;

namespace Garnet.Teams.Application.TeamProject.Commands
{
    public class TeamProjectJoinedCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamProjectRepository _teamProjectRepository;

        public TeamProjectJoinedCommand(ITeamRepository teamRepository, ITeamProjectRepository teamProjectRepository)
        {
            _teamRepository = teamRepository;
            _teamProjectRepository = teamProjectRepository;
        }

        public async Task<Result<TeamProject>> Execute(CancellationToken ct, string teamId, string projectId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            return await _teamProjectRepository.AddTeamProject(ct, projectId, teamId);
        }
    }
}