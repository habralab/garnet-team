namespace Garnet.Projects.Application.ProjectTeamParticipant.Args;

public record ProjectTeamParticipantCreateArgs(
    string TeamId,
    string TeamName,
    string ProjectId
    );
