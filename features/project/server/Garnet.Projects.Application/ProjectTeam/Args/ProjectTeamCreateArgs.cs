namespace Garnet.Projects.Application.ProjectTeam.Args;

public record ProjectTeamCreateArgs(
    string TeamId,
    string TeamName,
    string OwnerUserId,
    string TeamDescription,
    string? TeamAvatarUrl
    );
