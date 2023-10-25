namespace Garnet.Projects.Events.ProjectTask;

public record ProjectTaskUpdatedEvent(
    string Id,
    string ProjectId,
    string UserCreatorId,
    string Name,
    string? Description,
    string Status,
    string? TeamExecutorId,
    string? UserExecutorId,
    string[] Tags
);