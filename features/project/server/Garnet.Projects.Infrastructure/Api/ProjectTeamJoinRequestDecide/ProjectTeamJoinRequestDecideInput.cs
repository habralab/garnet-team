namespace Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestDecide;

public record ProjectTeamJoinRequestDecideInput(
    string ProjectTeamJoinRequestId,
    bool IsApproved
);