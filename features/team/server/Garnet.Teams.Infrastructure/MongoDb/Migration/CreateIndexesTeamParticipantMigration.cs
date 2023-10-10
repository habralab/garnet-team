using Garnet.Common.Infrastructure.Migrations;
using Garnet.Teams.Application.TeamParticipant;

namespace Garnet.Teams.Infrastructure.MongoDb.Migration
{
    public class CreateIndexesTeamParticipantMigration : IRepeatableMigration
    {
        private readonly ITeamParticipantRepository _teamParticipantRepository;

        public CreateIndexesTeamParticipantMigration(ITeamParticipantRepository teamParticipantRepository)
        {
            _teamParticipantRepository = teamParticipantRepository;
        }

        public async Task Execute(CancellationToken ct)
        {
            await _teamParticipantRepository.CreateIndexes(ct);
        }
    }
}