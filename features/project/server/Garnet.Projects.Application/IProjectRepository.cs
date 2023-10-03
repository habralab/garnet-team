namespace Garnet.Projects.Application;

public interface IProjectRepository
{
    Task<Project> CreateProject(CancellationToken ct, string ownerUserId, string projectName, string? description);
    Task<Project?> GetProject(CancellationToken ct, string projectId);
    Task<Project> EditProjectDescription(CancellationToken ct, string projectId, string? description);
    Task<Project?> DeleteProject(CancellationToken ct, string projectId);
    Task<Project> EditProjectOwner(CancellationToken ct, string projectId, string newOwnerUserId);
    Task CreateIndexes(CancellationToken ct);
}