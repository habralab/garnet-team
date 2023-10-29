using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Projects.Application.ProjectTask;
using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Infrastructure.MongoDb.Migrations;

public class CreateIndexesProjectTeamParticipantMigration : IRepeatableMigration
{
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public CreateIndexesProjectTeamParticipantMigration(IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }
    
    public async Task Execute(CancellationToken ct)
    {
        await _projectTeamParticipantRepository.CreateIndexes(ct);
    }
}