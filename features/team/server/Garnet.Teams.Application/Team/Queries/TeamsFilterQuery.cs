using Garnet.Teams.Application.Team.Args;

namespace Garnet.Teams.Application.Team.Queries
{
    public class TeamsFilterQuery
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsFilterQuery(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<TeamEntity[]> Query(CancellationToken ct, TeamFilterArgs args)
        {
            return await _teamRepository.FilterTeams(ct, args);
        }
    }
}