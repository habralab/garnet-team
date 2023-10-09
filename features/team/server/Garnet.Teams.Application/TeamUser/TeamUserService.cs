using FluentResults;
using Garnet.Teams.Application.Errors;

namespace Garnet.Teams.Application
{
    public class TeamUserService
    {
        private readonly ITeamUserRepository _usersRepository;

        public TeamUserService(ITeamUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<TeamUserEntity> AddUser(CancellationToken ct, string userId, string username)
        {
            return await _usersRepository.AddUser(ct, userId, username);
        }

        public async Task<Result<TeamUserEntity>> GetUser(CancellationToken ct, string userId)
        {
            var user = await _usersRepository.GetUser(ct, userId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(userId));
            }

            return Result.Ok(user);
        }

        public async Task<TeamUserEntity?> UpdateUser(CancellationToken ct, string userId, TeamUserUpdateArgs update)
        {
            return await _usersRepository.UpdateUser(ct, userId, update);
        }
    }
}