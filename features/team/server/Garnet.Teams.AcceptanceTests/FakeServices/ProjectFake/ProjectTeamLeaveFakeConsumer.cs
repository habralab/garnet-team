using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Events.TeamJoinProjectRequest;

namespace Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake
{
    public class ProjectTeamLeaveFakeConsumer : IMessageBusConsumer<TeamLeaveProjectEvent>
    {
        private readonly Dictionary<string, HashSet<string>> _projectTeams = new();

        public Task Consume(TeamLeaveProjectEvent message)
        {
            _projectTeams[message.ProjectId].Remove(message.TeamId);
            return Task.CompletedTask;
        }

        public void AddTeamToProject(string teamId, string projectId)
        {
            if (!_projectTeams.ContainsKey(projectId))
            {
                _projectTeams.Add(projectId, new());
            }

            _projectTeams[projectId].Add(teamId);
        }
    }
}