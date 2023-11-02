using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Users.Application.Errors;

namespace Garnet.Users.Application.Commands
{
    public class UserEditUsernameCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IUsersRepository _usersRepository;
        private readonly IMessageBus _messageBus;

        public UserEditUsernameCommand(
            IUsersRepository usersRepository,
            ICurrentUserProvider currentUserProvider,
            IMessageBus messageBus)
        {
            _usersRepository = usersRepository;
            _currentUserProvider = currentUserProvider;
            _messageBus = messageBus;
        }

        public async Task<Result<User>> Execute(string newUsername)
        {
            if (string.IsNullOrWhiteSpace(newUsername))
            {
                return Result.Fail(new UsernameCanNotBeEmptyError());
            }

            var user = await _usersRepository.GetUser(_currentUserProvider.UserId);
            if (user is null)
            {
                return Result.Fail(new UserNotFoundError(_currentUserProvider.UserId));
            }

            user = await _usersRepository.EditUsername(user.Id, newUsername);
            await _messageBus.Publish(user.ToUpdatedEvent());
            return Result.Ok(user);
        }
    }
}