namespace Garnet.Projects.Application.ProjectTeam;

public record ProjectTeamEntity(
    string Id,
    string TeamName,
    string OwnerUserId,
    string? TeamAvatarUrl,
    string[] UserParticipantIds
);