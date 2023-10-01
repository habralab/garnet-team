using Garnet.Common.Infrastructure.Support;
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

        public async Task<TeamParticipant> CreateTeamParticipant(CancellationToken ct, string userId, string teamId)
        {
            var db = _dbFactory.Create();
            var teamParticipant = TeamParticipantDocument.Create(Uuid.NewMongo(), userId, teamId);
            await db.TeamParticipants.InsertOneAsync(teamParticipant, cancellationToken: ct);
            return TeamParticipantDocument.ToDomain(teamParticipant);
        }

        public async Task<TeamParticipant[]> DeleteTeamParticipants(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var participants = await GetParticipantsFromTeam(ct, teamId);
            await db.TeamParticipants.DeleteManyAsync(
                _f.Eq(x => x.TeamId, teamId)
            );

            return participants;
        }

        public async Task<TeamParticipant[]> GetParticipantsFromTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var teamParticipants = await db.TeamParticipants.Find(x => x.TeamId == teamId).ToListAsync();
            return teamParticipants.Select(o => TeamParticipantDocument.ToDomain(o)).ToArray();
        }
    }
}