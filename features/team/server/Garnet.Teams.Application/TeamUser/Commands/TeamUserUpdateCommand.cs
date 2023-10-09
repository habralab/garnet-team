using Garnet.Teams.Application.TeamUser.Args;

namespace Garnet.Teams.Application.TeamUser.Commands
{
    public class TeamUserUpdateCommand
    {
        private readonly ITeamUserRepository _usersRepository;

        public TeamUserUpdateCommand(ITeamUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<TeamUserEntity?> Execute(CancellationToken ct, string userId, TeamUserUpdateArgs update)
        {
            return await _usersRepository.UpdateUser(ct, userId, update);
        }
    }
}