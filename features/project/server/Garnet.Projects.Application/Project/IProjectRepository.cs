using Garnet.Projects.Application.Args;

namespace Garnet.Projects.Application.Project;

public interface IProjectRepository
{
    Task<ProjectEntity> CreateProject(CancellationToken ct, ProjectCreateArgs args);
    Task<ProjectEntity?> GetProject(CancellationToken ct, string projectId);
    Task<ProjectEntity> EditProjectDescription(CancellationToken ct, string projectId, string? description);
    Task<ProjectEntity?> DeleteProject(CancellationToken ct, string projectId);
    Task<ProjectEntity> EditProjectOwner(CancellationToken ct, string projectId, string newOwnerUserId);
    Task<ProjectEntity> EditProjectAvatar(CancellationToken ct, string projectId, string avatarUrl);
    Task<ProjectEntity[]> FilterProjects(CancellationToken ct, ProjectFilterArgs args);
    Task CreateIndexes(CancellationToken ct);
}