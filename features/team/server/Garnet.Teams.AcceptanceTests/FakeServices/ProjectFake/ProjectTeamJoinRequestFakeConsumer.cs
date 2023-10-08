using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Events;

namespace Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake
{
    public class ProjectTeamJoinRequestFakeConsumer : IMessageBusConsumer<TeamJoinProjectRequestCreatedEvent>
    {
        private readonly Dictionary<string, HashSet<string>> _projectTeams = new();

        public Task Consume(TeamJoinProjectRequestCreatedEvent message)
        {
            AddTeamToProject(message.TeamId, message.ProjectId);
            return Task.CompletedTask;
        }

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