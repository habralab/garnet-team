namespace Garnet.Projects.Events.ProjectTeamJoinRequest;

public record ProjectTeamJoinRequestDecidedEvent(
    string Id,
    string TeamId,
    string ProjectId,
    bool IsApproved
    );