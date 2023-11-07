namespace Garnet.Teams.Application.ProjectTeamParticipant.Queries
{
    public class TeamProjectListByTeamsQuery
    {
        private readonly ITeamProjectRepository _teamProjectRepository;

        public TeamProjectListByTeamsQuery(ITeamProjectRepository teamProjectRepository)
        {
            _teamProjectRepository = teamProjectRepository;
        }

        public async Task<Dictionary<string, ProjectTeamParticipantEntity[]>> Query(CancellationToken ct, string[] teamIds)
        {
            var projects = await _teamProjectRepository.TeamProjectListOfTeams(ct, teamIds);
            var projectsByTeams = projects.GroupBy(x => x.TeamId).ToDictionary(x => x.Key, y => y.ToArray());
            return projectsByTeams;
        }
    }
}