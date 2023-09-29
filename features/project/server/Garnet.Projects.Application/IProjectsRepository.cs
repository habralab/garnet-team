namespace Garnet.Projects.Application;

public interface IProjectsRepository
{
    Task<Project> CreateProject(CancellationToken ct, string ownerUserId, string projectName, string? description);
    Task CreateIndexes(CancellationToken ct);
}