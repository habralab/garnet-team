namespace Garnet.Projects.Application;

public record ProjectTeamParticipant(
    string Id,
    string TeamId,
    string TeamName,
    string ProjectId
);