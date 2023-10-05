namespace Garnet.Projects.Infrastructure.Api.ProjectTeamParticipant;

public record ProjectTeamParticipantPayload(
    string Id,
    string TeamId,
    string TeamName,
    string ProjectId
    );