using FluentResults;
using Garnet.Users.Application.Errors;

namespace Garnet.Users.Application.Queries
{
    public class UserGetQuery
    {
        private readonly IUsersRepository _usersRepository;

        public UserGetQuery(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Result<User>> Query(CancellationToken ct, string userId)
        {
            var user = await _usersRepository.GetUser(ct, userId);

            return user is null ? Result.Fail(new UserNotFoundError(userId)) : Result.Ok(user);
        }
    }
}