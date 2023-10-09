using FluentResults;
using Garnet.Teams.Application.Team.Errors;

namespace Garnet.Teams.Application.Team.Queries
{
    public class TeamGetQuery
    {
        private readonly ITeamRepository _teamRepository;

        public TeamGetQuery(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Result<TeamEntity>> Query(CancellationToken ct, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);

            return team is null ? Result.Fail(new TeamNotFoundError(teamId)) : Result.Ok(team);
        }

    }
}