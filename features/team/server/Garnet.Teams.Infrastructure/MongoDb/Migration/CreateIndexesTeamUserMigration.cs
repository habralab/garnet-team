using Garnet.Common.Infrastructure.Migrations;
using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb.Migration
{
    public class CreateIndexesTeamUserMigration : IRepeatableMigration
    {
        private readonly ITeamUserRepository _userRepository;
        public CreateIndexesTeamUserMigration(ITeamUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(CancellationToken ct)
        {
            await _userRepository.CreateIndexes(ct);
        }
    }
}