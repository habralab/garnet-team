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

        public async Task<TeamJoinProjectRequestEntity> CreateJoinProjectRequest(CancellationToken ct, string teamId, string projectId)
        {
            var db = _dbFactory.Create();
            var joinProjectRequest = TeamJoinProjectRequestDocument.Create(Uuid.NewMongo(), teamId, projectId);
            await db.TeamJoinProjectRequests.InsertOneAsync(joinProjectRequest, cancellationToken: ct);

            return TeamJoinProjectRequestDocument.ToDomain(joinProjectRequest);
        }

        public async Task<TeamJoinProjectRequestEntity?> DeleteJoinProjectRequestById(CancellationToken ct, string joinProjectRequestId)
        {
            var db = _dbFactory.Create();
            var joinProjectRequest = await db.TeamJoinProjectRequests.FindOneAndDeleteAsync(
                _f.Eq(x => x.Id, joinProjectRequestId),
                cancellationToken: ct
            );

            return joinProjectRequest is null ? null : TeamJoinProjectRequestDocument.ToDomain(joinProjectRequest);
        }

        public async Task DeleteJoinProjectRequestByProject(CancellationToken ct, string projectId)
        {
            var db = _dbFactory.Create();
            await db.TeamJoinProjectRequests.DeleteManyAsync(
                _f.Eq(x => x.ProjectId, projectId),
                cancellationToken: ct
            );
        }

        public async Task<TeamJoinProjectRequestEntity[]> GetJoinProjectRequestsByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var teamFilter = _f.Eq(x => x.TeamId, teamId);
            var joinProjectRequests = await db.TeamJoinProjectRequests
                .Find(teamFilter)
                .ToListAsync(ct);

            return joinProjectRequests.Select(x => TeamJoinProjectRequestDocument.ToDomain(x)).ToArray();
        }
    }
}