
namespace Garnet.Teams.Application.TeamUser.Commands
{
    public class TeamUserCreateCommand
    {
        private readonly ITeamUserRepository _usersRepository;

        public TeamUserCreateCommand(ITeamUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<TeamUserEntity> Execute(CancellationToken ct, string userId, string username)
        {
            return await _usersRepository.AddUser(ct, userId, username);
        }

    }
}