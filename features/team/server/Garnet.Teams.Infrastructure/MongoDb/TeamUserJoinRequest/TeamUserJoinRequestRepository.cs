using Garnet.Common.Application;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application.TeamUserJoinRequest;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamUserJoinRequest
{
    public class TeamUserJoinRequestRepository : RepositoryBase, ITeamUserJoinRequestRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamUserJoinRequestDocument> _f = Builders<TeamUserJoinRequestDocument>.Filter;

        public TeamUserJoinRequestRepository(
            ICurrentUserProvider currentUserProvider,
            IDateTimeService dateTimeService,
            DbFactory dbFactory) : base(currentUserProvider, dateTimeService)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamUserJoinRequestEntity> CreateJoinRequestByUser(CancellationToken ct, string userId, string teamId)
        {
            var db = _dbFactory.Create();
            var joinRequest = TeamUserJoinRequestDocument.Create(
                Uuid.NewMongo(),
                userId,
                teamId
            );

            joinRequest = await InsertOneDocument(
                ct,
                db.TeamUserJoinRequests,
                joinRequest);

            return TeamUserJoinRequestDocument.ToDomain(joinRequest);
        }

        public async Task<TeamUserJoinRequestEntity?> DeleteUserJoinRequestById(CancellationToken ct, string userJoinRequestId)
        {
            var db = _dbFactory.Create();
            var joinRequest = await db.TeamUserJoinRequests.FindOneAndDeleteAsync(
                _f.Eq(x => x.Id, userJoinRequestId),
                cancellationToken: ct
            );

            return joinRequest is null ? null : TeamUserJoinRequestDocument.ToDomain(joinRequest);
        }

        public async Task DeleteUserJoinRequestsByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            await db.TeamUserJoinRequests.DeleteManyAsync(
                _f.Eq(x => x.TeamId, teamId),
                cancellationToken: ct
            );
        }

        public async Task<TeamUserJoinRequestEntity[]> GetAllUserJoinRequestsByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var joinRequests = await db.TeamUserJoinRequests.Find(
                _f.Eq(x => x.TeamId, teamId)
            ).ToListAsync(ct);

            return joinRequests.Select(x => TeamUserJoinRequestDocument.ToDomain(x)).ToArray();
        }

        public async Task<TeamUserJoinRequestEntity?> GetUserJoinRequestById(CancellationToken ct, string userJoinRequestId)
        {
            var db = _dbFactory.Create();
            var joinRequest = await db.TeamUserJoinRequests.Find(
                _f.Eq(x => x.Id, userJoinRequestId)
            ).FirstOrDefaultAsync(ct);

            return joinRequest is null ? null : TeamUserJoinRequestDocument.ToDomain(joinRequest);
        }
    }
}