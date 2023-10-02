namespace Garnet.Projects.Application;

public interface IProjectsRepository
{
    Task<Project> CreateProject(CancellationToken ct, string ownerUserId, string projectName, string? description);
    Task<Project?> GetProject(CancellationToken ct, string projectId);
    Task<Project?> DeleteProject(CancellationToken ct, string projectId);
    Task CreateIndexes(CancellationToken ct);
}