namespace Garnet.Projects.Events.ProjectTask;

public record ProjectTaskUpdatedEvent(
    string Id,
    int TaskNumber,
    string ProjectId,
    string ResponsibleUserId,
    string Name,
    string? Description,
    string Status,
    string[] TeamExecutorIds,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels,
    bool Reopened
);