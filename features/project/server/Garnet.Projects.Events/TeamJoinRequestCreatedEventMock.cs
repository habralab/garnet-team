namespace Garnet.Projects.Events;

public record TeamJoinRequestCreatedEventMock(
    string TeamId,
    string TeamName,
    string ProjectId
);