using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamUserJoinRequestRepository : ITeamUserJoinRequestRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamUserJoinRequestDocument> _f = Builders<TeamUserJoinRequestDocument>.Filter;

        public TeamUserJoinRequestRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamUserJoinRequest> CreateJoinRequestByUser(CancellationToken ct, string userId, string teamId)
        {
            var db = _dbFactory.Create();
            var joinRequest = TeamUserJoinRequestDocument.Create(
                Uuid.NewMongo(),
                userId,
                teamId
            );

            await db.TeamUserJoinRequests.InsertOneAsync(joinRequest, cancellationToken: ct);

            return TeamUserJoinRequestDocument.ToDomain(joinRequest);
        }

        public async Task<TeamUserJoinRequest[]> GetAllUserJoinRequestsByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var joinRequests = await db.TeamUserJoinRequests.Find(
                _f.Eq(x => x.TeamId, teamId)
            ).ToListAsync(ct);

            return joinRequests.Select(x => TeamUserJoinRequestDocument.ToDomain(x)).ToArray();
        }
    }
}