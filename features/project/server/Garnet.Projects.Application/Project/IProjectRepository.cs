using Garnet.Projects.Application.Args;
using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Application;

public interface IProjectRepository
{
    Task<ProjectEntity> CreateProject(CancellationToken ct, ProjectCreateArgs args);
    Task<ProjectEntity?> GetProject(CancellationToken ct, string projectId);
    Task<ProjectEntity> EditProjectDescription(CancellationToken ct, string projectId, string? description);
    Task<ProjectEntity?> DeleteProject(CancellationToken ct, string projectId);
    Task<ProjectEntity> EditProjectOwner(CancellationToken ct, string projectId, string newOwnerUserId);
    Task<ProjectEntity[]> FilterProjects(CancellationToken ct, string? search, string[] tags, int skip, int take);
    Task CreateIndexes(CancellationToken ct);
}