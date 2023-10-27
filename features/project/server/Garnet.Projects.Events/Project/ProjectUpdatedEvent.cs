namespace Garnet.Projects.Events.Project;

public record ProjectUpdatedEvent(
    string ProjectId,
    string ProjectName,
    string OwnerUserId,
    string? Description,
    string? AvatarUrl,
    string[] Tags,
    int TasksCounter
);