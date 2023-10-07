namespace Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake
{
    public class ProjectFake
    {
        private readonly Dictionary<string, HashSet<string>> _projectTeams = new();

        public void CreateProject(string projectId)
        {
            _projectTeams.Add(projectId, new());
        }

        public void AddTeamToProject(string teamId, string projectId)
        {
            _projectTeams[projectId].Add(teamId);
        }

        public List<string> GetProjectTeams(string projectId) => _projectTeams[projectId].ToList();
    }
}