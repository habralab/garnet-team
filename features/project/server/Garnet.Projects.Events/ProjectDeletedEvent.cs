namespace Garnet.Projects.Events;

public record ProjectDeletedEvent(
    string ProjectId,
    string ProjectName,
    string OwnerUserId,
    string? Description
);
