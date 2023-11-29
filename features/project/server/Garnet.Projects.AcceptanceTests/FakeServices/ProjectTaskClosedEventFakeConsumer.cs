using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events.ProjectTask;

namespace Garnet.Projects.AcceptanceTests.FakeServices;

public class ProjectTaskClosedEventFakeConsumer : IMessageBusConsumer<ProjectTaskClosedEvent>
{
    private ProjectTaskClosedEvent? _message;

    public Task Consume(ProjectTaskClosedEvent message)
    {
        _message = message;
        return Task.CompletedTask;
    }

    public ProjectTaskClosedEvent GetMessage()
    {
        return _message!;
    }
}