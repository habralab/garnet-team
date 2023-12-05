using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Args;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamUser
{
    public class TeamUserRepository : ITeamUserRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamUserDocument> _f = Builders<TeamUserDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamUserDocument> _u = Builders<TeamUserDocument>.Update;
        private readonly IndexKeysDefinitionBuilder<TeamUserDocument> _i = Builders<TeamUserDocument>.IndexKeys;

        public TeamUserRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamUserEntity> AddUser(CancellationToken ct, TeamUserCreateArgs args)
        {
            var db = _dbFactory.Create();

            var user = TeamUserDocument.Create(
                args.UserId,
                args.Username,
                string.Empty
            );
            await db.TeamUsers.InsertOneAsync(
                user,
                cancellationToken: ct
            );

            return TeamUserDocument.ToDomain(user);
        }

        public async Task<TeamUserEntity?> GetUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();
            var user = await db.TeamUsers.Find(x => x.Id == userId).FirstOrDefaultAsync(ct);

            return user is null ? null : TeamUserDocument.ToDomain(user);
        }

        public async Task<TeamUserEntity[]> TeamUserListByIds(CancellationToken ct, string[] userIds)
        {
            var db = _dbFactory.Create();
            var users = await db.TeamUsers.Find(
                _f.In(x => x.Id, userIds)
            ).ToListAsync(ct);

            return users.Select(x => TeamUserDocument.ToDomain(x)).ToArray();
        }

        public async Task<TeamUserEntity?> UpdateUser(CancellationToken ct, string userId, TeamUserUpdateArgs update)
        {
            var db = _dbFactory.Create();
            var updatedUser = await db.TeamUsers
                .FindOneAndUpdateAsync(
                    _f.Eq(x => x.Id, userId),
                    _u
                        .Set(x => x.Username, update.Username)
                        .Set(x => x.Tags, update.Tags)
                        .Set(x => x.AvatarUrl, update.AvatarUrl),
                    options: new FindOneAndUpdateOptions<TeamUserDocument>
                    {
                        ReturnDocument = ReturnDocument.After,
                        IsUpsert = true
                    },
                    cancellationToken: ct
                );

            return updatedUser is null ? null : TeamUserDocument.ToDomain(updatedUser);
        }
    }
}