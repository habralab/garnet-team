using Garnet.Common.Infrastructure.Migrations;
using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb.Migrations;

public class CreateIndexesMigration : IRepeatableMigration
{
    private readonly IProjectRepository _projectRepository;

    public CreateIndexesMigration(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task Execute(CancellationToken ct)
    {
        await _projectRepository.CreateIndexes(ct);
    }
}