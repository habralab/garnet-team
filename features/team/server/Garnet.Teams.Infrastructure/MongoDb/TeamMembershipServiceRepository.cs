using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamMembershipServiceRepository : ITeamMembershipRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamUserJoinRequestDocument> _f = Builders<TeamUserJoinRequestDocument>.Filter;

        public TeamMembershipServiceRepository(DbFactory dbFactory)
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

            await db.UserJoinTeamRequest.InsertOneAsync(joinRequest, cancellationToken: ct);

            return TeamUserJoinRequestDocument.ToDomain(joinRequest);
        }

        public async Task<TeamUserJoinRequest[]> GetAllUserJoinRequestByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var joinRequests = await db.UserJoinTeamRequest.Find(
                _f.Eq(x => x.TeamId, teamId)
            ).ToListAsync(ct);

            return joinRequests.Select(x => TeamUserJoinRequestDocument.ToDomain(x)).ToArray();
        }
    }
}