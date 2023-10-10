using Garnet.Projects.Events;

namespace Garnet.Projects.Application;

public record ProjectTeamJoinRequest(
    string Id,
    string TeamId,
    string TeamName,
    string ProjectId
);