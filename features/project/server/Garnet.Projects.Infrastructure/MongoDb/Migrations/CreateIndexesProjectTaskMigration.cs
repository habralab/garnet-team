using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Projects.Application.ProjectTask;

namespace Garnet.Projects.Infrastructure.MongoDb.Migrations;

public class CreateIndexesProjectTaskMigration : IRepeatableMigration
{
    private readonly IProjectTaskRepository _projectTaskRepository;

    public CreateIndexesProjectTaskMigration(IProjectTaskRepository projectTaskRepository)
    {
        _projectTaskRepository = projectTaskRepository;
    }
    
    public async Task Execute(CancellationToken ct)
    {
        await _projectTaskRepository.CreateIndexes(ct);
    }
}