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

        public async Task<TeamUser> AddUser(CancellationToken ct, string userId, string username)
        {
            return await _usersRepository.AddUser(ct, userId, username);
        }

        public async Task<TeamUser[]> FindUsers(CancellationToken ct, TeamUserFilterParams filter)
        {
            return await _usersRepository.FilterUsers(ct, filter);
        }

        public async Task<Result<TeamUser>> GetUser(CancellationToken ct, string userId)
        {
            var user = await _usersRepository.GetUser(ct, userId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(userId));
            }

            return Result.Ok(user);
        }
    }
}