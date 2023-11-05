using FluentResults;
using Garnet.Common.Application.MessageBus;
using Garnet.Users.Application.Errors;

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

        public async Task<Result<User>> Execute(string identityId, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return Result.Fail(new UsernameCanNotBeEmptyError());
            }

            var user = await _usersRepository.CreateUser(identityId, username);
            await _messageBus.Publish(user.ToCreatedEvent());
            return Result.Ok(user);
        }
    }
}