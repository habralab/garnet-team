using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Infrastructure.MongoDb.Migrations;

public class CreateIndexesProjectMigration : IRepeatableMigration
{
    private readonly IProjectRepository _projectRepository;

    public CreateIndexesProjectMigration(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task Execute(CancellationToken ct)
    {
        await _projectRepository.CreateIndexes(ct);
    }
}