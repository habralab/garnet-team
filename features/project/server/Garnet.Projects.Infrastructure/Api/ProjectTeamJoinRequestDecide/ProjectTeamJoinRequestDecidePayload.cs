namespace Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestDecide;

public record ProjectTeamJoinRequestDecidePayload(
    string Id,
    string TeamId,
    string TeamName,
    string ProjectId
    );