namespace Garnet.Projects.Events;

public record ProjectTeamJoinRequestDecidedEvent(
    string Id,
    string TeamId,
    string ProjectId,
    bool IsApproved
    );