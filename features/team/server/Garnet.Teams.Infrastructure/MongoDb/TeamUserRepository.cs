using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamUserRepository : ITeamUserRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<string> _f = Builders<string>.Filter;

        public TeamUserRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamUser> AddUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();

            var user = TeamUserDocument.Create(Uuid.NewMongo(), userId);
            await db.TeamUsers.InsertOneAsync(
                user,
                cancellationToken: ct
            );

            return TeamUserDocument.ToDomain(user);
        }

        public async Task<TeamUser?> GetUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();
            var user = await db.TeamUsers.Find(x => x.UserId == userId).FirstOrDefaultAsync(ct);

            return user is null ? null : TeamUserDocument.ToDomain(user);
        }
    }
}