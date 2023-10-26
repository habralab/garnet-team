using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Application.ProjectTeamParticipant;

public record ProjectTeamParticipantEntity(
    string Id,
    string TeamId,
    string TeamName,
    string ProjectId,
    string? TeamAvatarUrl,
    ProjectUserEntity[] UserParticipants,
    ProjectEntity[] Projects
);