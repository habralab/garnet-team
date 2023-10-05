namespace Garnet.Projects.Events;

public record TeamUpdatedEventMock(
    string Id,
    string Name,
    string Description,
    string OwnerUserId,
    string[] Tags
);