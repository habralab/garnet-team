using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamUserRepository : ITeamUserRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<string> _f = Builders<string>.Filter;
        private readonly IndexKeysDefinitionBuilder<TeamUserDocument> _i = Builders<TeamUserDocument>.IndexKeys;

        public TeamUserRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamUser> AddUser(CancellationToken ct, string userId, string username)
        {
            var db = _dbFactory.Create();

            var user = TeamUserDocument.Create(Uuid.NewMongo(), userId, username);
            await db.TeamUsers.InsertOneAsync(
                user,
                cancellationToken: ct
            );

            return TeamUserDocument.ToDomain(user);
        }

        public async Task CreateIndexes(CancellationToken ct)
        {
            var db = _dbFactory.Create();
            await db.TeamUsers.Indexes.CreateOneAsync(
                new CreateIndexModel<TeamUserDocument>(
                    _i.Text(x => x.Username)
                )
            );
        }

        public async Task<TeamUser?> GetUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();
            var user = await db.TeamUsers.Find(x => x.UserId == userId).FirstOrDefaultAsync(ct);

            return user is null ? null : TeamUserDocument.ToDomain(user);
        }
    }
}