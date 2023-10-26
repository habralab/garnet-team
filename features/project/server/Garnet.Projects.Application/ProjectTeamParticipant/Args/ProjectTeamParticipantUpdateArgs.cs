namespace Garnet.Projects.Application.ProjectTeamParticipant.Args;

public record ProjectTeamParticipantUpdateArgs(
    string TeamId,
    string TeamName,
    string? TeamAvatarUrl
);