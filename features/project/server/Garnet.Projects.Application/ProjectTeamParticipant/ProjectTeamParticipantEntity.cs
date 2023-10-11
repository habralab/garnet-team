namespace Garnet.Projects.Application.ProjectTeamParticipant;

public record ProjectTeamParticipantEntity(
    string Id,
    string TeamId,
    string TeamName,
    string ProjectId
);