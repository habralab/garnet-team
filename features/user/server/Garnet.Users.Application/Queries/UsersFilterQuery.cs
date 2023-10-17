using Garnet.Users.Application.Args;

namespace Garnet.Users.Application.Queries
{
    public class UsersFilterQuery
    {
        private readonly IUsersRepository _usersRepository;

        public UsersFilterQuery(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User[]> Query(CancellationToken ct, UserFilterArgs args)
        {
            return await _usersRepository.FilterUsers(ct, args);
        }
    }
}