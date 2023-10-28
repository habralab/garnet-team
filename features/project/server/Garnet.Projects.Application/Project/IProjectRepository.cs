using Garnet.Projects.Application.Project.Args;

namespace Garnet.Projects.Application.Project;

public interface IProjectRepository
{
    Task<ProjectEntity> CreateProject(CancellationToken ct, string ownerUserId, ProjectCreateArgs args);
    Task<ProjectEntity?> GetProject(CancellationToken ct, string projectId);
    Task<ProjectEntity> EditProjectDescription(CancellationToken ct, string projectId, string? description);
    Task<ProjectEntity> EditProjectName(CancellationToken ct, string projectId, string newName);
    Task<ProjectEntity> EditProjectTags(CancellationToken ct, string projectId, string[] tags);
    Task<ProjectEntity?> DeleteProject(CancellationToken ct, string projectId);
    Task<ProjectEntity> EditProjectOwner(CancellationToken ct, string projectId, string newOwnerUserId);
    Task<ProjectEntity> EditProjectAvatar(CancellationToken ct, string projectId, string avatarUrl);
    Task<ProjectEntity[]> FilterProjects(CancellationToken ct, ProjectFilterArgs args);
    Task CreateIndexes(CancellationToken ct);
}