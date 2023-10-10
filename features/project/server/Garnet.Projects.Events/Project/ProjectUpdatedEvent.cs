namespace Garnet.Projects.Events;

public record ProjectUpdatedEvent(
    string ProjectId,
    string ProjectName,
    string OwnerUserId,
    string? Description
);
