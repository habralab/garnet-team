using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamUserRepository : ITeamUserRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamUserDocument> _f = Builders<TeamUserDocument>.Filter;
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

        public async Task<TeamUser[]> FilterUsers(CancellationToken ct, TeamUserFilterParams filter)
        {
            var db = _dbFactory.Create();

            var searchFilter = filter.Search is null
                ? _f.Empty
                : _f.Where(x => x.Username.ToLower().Contains(filter.Search.ToLower()));

            var userIdFilter = filter.UserIds is null
                ? _f.Empty
                : _f.Where(x => filter.UserIds.Contains(x.UserId));

            var users = await db.TeamUsers
                .Find(searchFilter & userIdFilter)
                .Skip(filter.Skip)
                .Limit(filter.Take)
                .ToListAsync(ct);

            return users.Select(x => TeamUserDocument.ToDomain(x)).ToArray();
        }

        public async Task<TeamUser?> GetUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();
            var user = await db.TeamUsers.Find(x => x.UserId == userId).FirstOrDefaultAsync(ct);

            return user is null ? null : TeamUserDocument.ToDomain(user);
        }
    }
}