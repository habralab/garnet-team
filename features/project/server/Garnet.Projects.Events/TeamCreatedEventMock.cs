namespace Garnet.Projects.Events;

public record TeamCreatedEventMock(
    string Id,
    string Name,
    string Description,
    string OwnerUserId,
    string[] Tags
);
