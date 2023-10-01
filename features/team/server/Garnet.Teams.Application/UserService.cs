namespace Garnet.Teams.Application
{
    public class UserService
    {
        private readonly IUserRepository _usersRepository;

        public UserService(IUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<string> AddUser(CancellationToken ct, string userId)
        {
            return await _usersRepository.AddUser(ct, userId);
        }

        public async Task<string?> GetUser(CancellationToken ct, string userId)
        {
            return await _usersRepository.GetUser(ct, userId);
        }
    }
}