using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamParticipantRepository : ITeamParticipantRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamParticipantDocument> _f = Builders<TeamParticipantDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamParticipantDocument> _u = Builders<TeamParticipantDocument>.Update;
        private readonly IndexKeysDefinitionBuilder<TeamParticipantDocument> _i = Builders<TeamParticipantDocument>.IndexKeys;

        public TeamParticipantRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Task<TeamParticipant> CreateTeamParticipant(CancellationToken ct, string userId, string teamId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamParticipant[]> GetParticipantsFromTeam(CancellationToken ct, string teamId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateIndexes(CancellationToken ct)
        {
            var db = _dbFactory.Create();
            await db.TeamParticipants.Indexes.CreateOneAsync(
                new CreateIndexModel<TeamParticipantDocument>(
                _i.Text(o => o.TeamId)
                ),
                cancellationToken: ct
            );
        }
    }
}