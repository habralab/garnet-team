using Garnet.Common.Application.MessageBus;

namespace Garnet.Users.Application.Commands
{
    public class UserCreateCommand
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMessageBus _messageBus;

        public UserCreateCommand(
            IUsersRepository usersRepository,
            IMessageBus messageBus)
        {
            _usersRepository = usersRepository;
            _messageBus = messageBus;
        }

        public async Task<User> Execute(CancellationToken ct, string identityId, string username)
        {
            var user = await _usersRepository.CreateUser(ct, identityId, username);
            await _messageBus.Publish(user.ToCreatedEvent());
            return user;
        }
    }
}