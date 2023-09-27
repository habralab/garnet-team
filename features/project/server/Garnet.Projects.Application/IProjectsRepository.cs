namespace Garnet.Projects.Application;

public interface IProjectsRepository
{
    Task<Project> CreateProject(CancellationToken ct, string ownerUserId, string projectName);
    Task CreateIndexes(CancellationToken ct);
}