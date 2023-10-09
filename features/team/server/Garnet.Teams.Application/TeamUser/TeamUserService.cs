using FluentResults;
using Garnet.Teams.Application.TeamUser.Args;
using Garnet.Teams.Application.TeamUser.Entities;
using Garnet.Teams.Application.TeamUser.Errors;

namespace Garnet.Teams.Application.TeamUser
{
    public class TeamUserService
    {
        private readonly ITeamUserRepository _usersRepository;

        public TeamUserService(ITeamUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
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