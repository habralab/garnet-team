using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class UserRepository : IUserRepository
    {
        public Task<string> AddUser(CancellationToken ct, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetUser(CancellationToken ct, string userId)
        {
            throw new NotImplementedException();
        }
    }
}