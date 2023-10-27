namespace Garnet.Projects.Events.ProjectTask;

public record ProjectTaskDeletedEvent(
    string Id,
    int TaskNumber,
    string ProjectId,
    string UserCreatorId,
    string Name,
    string? Description,
    string Status,
    string? TeamExecutorId,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels
);