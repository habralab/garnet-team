using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class UserRepository : IUserRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<string> _f = Builders<string>.Filter;

        public UserRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<string> AddUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();
            await db.Users.InsertOneAsync(
                userId,
                cancellationToken: ct
            );

            return userId;
        }

        public async Task<string?> GetUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();
            return await db.Users.Find(x => x == userId).FirstOrDefaultAsync(ct);
        }
    }
}