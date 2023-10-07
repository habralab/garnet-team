using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamJoinProjectRequestRepository : ITeamJoinProjectRequestRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamJoinProjectRequestDocument> _f = Builders<TeamJoinProjectRequestDocument>.Filter;

        public TeamJoinProjectRequestRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamJoinProjectRequest> CreateJoinProjectRequest(CancellationToken ct, string teamId, string projectId)
        {
            var db = _dbFactory.Create();
            var joinProjectRequest = TeamJoinProjectRequestDocument.Create(Uuid.NewMongo(), teamId, projectId);
            await db.TeamJoinProjectRequests.InsertOneAsync(joinProjectRequest, cancellationToken: ct);

            return TeamJoinProjectRequestDocument.ToDomain(joinProjectRequest);
        }

        public async Task<TeamJoinProjectRequest[]> GetJoinProjectRequestsByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var teamFilter = _f.Eq(x=> x.TeamId, teamId);
            var joinProjectRequests = await db.TeamJoinProjectRequests
                .Find(teamFilter)
                .ToListAsync(ct);

            return joinProjectRequests.Select(x=> TeamJoinProjectRequestDocument.ToDomain(x)).ToArray();
        }
    }
}