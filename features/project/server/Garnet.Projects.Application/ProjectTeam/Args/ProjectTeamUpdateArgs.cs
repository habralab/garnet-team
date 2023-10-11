namespace Garnet.Projects.Application.ProjectTeam.Args;

public record ProjectTeamUpdateArgs(
    string TeamId,
    string TeamName,
    string OwnerUserId
);