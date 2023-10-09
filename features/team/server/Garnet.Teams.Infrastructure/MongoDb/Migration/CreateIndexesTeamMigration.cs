using Garnet.Common.Infrastructure.Migrations;
using Garnet.Teams.Application;
using Garnet.Teams.Application.Team;

namespace Garnet.Teams.Infrastructure.MongoDb.Migration
{
    public class CreateIndexesTeamMigration : IRepeatableMigration
    {
        private readonly ITeamRepository _repository;

        public CreateIndexesTeamMigration(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(CancellationToken ct)
        {
            await _repository.CreateIndexes(ct);
        }
    }
}