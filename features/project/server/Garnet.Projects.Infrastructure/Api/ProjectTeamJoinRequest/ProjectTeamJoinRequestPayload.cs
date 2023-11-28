namespace Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequest;

public record ProjectTeamJoinRequestPayload(
    string Id,
    string TeamId,
    string TeamName,
    string TeamDescription,
    string? TeamAvatarUrl,
    string ProjectId,
    int ProjectCount,
    int TeamUserParticipants
    );