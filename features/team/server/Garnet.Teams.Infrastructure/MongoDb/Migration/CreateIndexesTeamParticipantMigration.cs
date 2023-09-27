using Garnet.Common.Infrastructure.Migrations;
using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb.Migration
{
    public class CreateIndexesTeamParticipantMigration : IRepeatableMigration
    {
        private readonly ITeamParticipantRepository _repository;

        public CreateIndexesTeamParticipantMigration(ITeamParticipantRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(CancellationToken ct)
        {
            await _repository.CreateIndexes(ct);
        }
    }
}