using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Events;

namespace Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake
{
    public class ProjectTeamJoinRequestFakeConsumer : IMessageBusConsumer<TeamJoinProjectRequestCreatedEvent>
    {
        private readonly ProjectFake _projectFake;
        public ProjectTeamJoinRequestFakeConsumer(ProjectFake projectFake)
        {
            _projectFake = projectFake;
        }

        public Task Consume(TeamJoinProjectRequestCreatedEvent message)
        {
            _projectFake.AddTeamToProject(message.TeamId, message.ProjectId);
            return Task.CompletedTask;
        }
    }
}