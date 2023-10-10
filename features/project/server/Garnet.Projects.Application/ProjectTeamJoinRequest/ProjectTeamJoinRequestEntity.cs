using Garnet.Projects.Events;

namespace Garnet.Projects.Application;

public record ProjectTeamJoinRequestEntity(
    string Id,
    string TeamId,
    string TeamName,
    string ProjectId
);