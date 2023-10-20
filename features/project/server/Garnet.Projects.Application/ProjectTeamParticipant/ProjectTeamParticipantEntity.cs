using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Application.ProjectTeamParticipant;

public record ProjectTeamParticipantEntity(
    string Id,
    string TeamId,
    string TeamName,
    string ProjectId,
    UserParticipant[] UserParticipants,
    ProjectEntity[] Projects
);

public record UserParticipant(
    string UserId,
    string UserName,
    string UserAvatarUrl
    );