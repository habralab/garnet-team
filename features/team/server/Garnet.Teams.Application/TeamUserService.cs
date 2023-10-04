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

        public async Task<TeamUser> AddUser(CancellationToken ct, string userId)
        {
            return await _usersRepository.AddUser(ct, userId);
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