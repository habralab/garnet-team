namespace Garnet.Teams.Application.TeamProject.Queries
{
    public class TeamProjectGetByTeamQuery
    {
        private readonly ITeamProjectRepository _teamProjectRepository;

        public TeamProjectGetByTeamQuery(ITeamProjectRepository teamProjectRepository)
        {
            _teamProjectRepository = teamProjectRepository;
        }

        public async Task<TeamProject[]> Query(CancellationToken ct, string teamId)
        {
            return await _teamProjectRepository.GetTeamProjectByTeam(ct, teamId);
        }
    }
}