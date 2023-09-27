using Garnet.Common.Infrastructure.Migrations;
using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb.Migrations;

public class CreateIndexesMigration : IRepeatableMigration
{
    private readonly IProjectsRepository _projectsRepository;

    public CreateIndexesMigration(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }
    
    public async Task Execute(CancellationToken ct)
    {
        await _projectsRepository.CreateIndexes(ct);
    }
}