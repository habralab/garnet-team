using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Users.Application.Errors;

namespace Garnet.Users.Application.Commands
{
    public class UserEditDescriptionCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IUsersRepository _usersRepository;
        private readonly IMessageBus _messageBus;

        public UserEditDescriptionCommand(
            ICurrentUserProvider currentUserProvider,
            IUsersRepository usersRepository,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _usersRepository = usersRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<User>> Execute(CancellationToken ct, string description)
        {
            var user = await _usersRepository.GetUser(ct, _currentUserProvider.UserId);
            if (user is null)
            {
                return Result.Fail(new UserNotFoundError(_currentUserProvider.UserId));
            }

            user = await _usersRepository.EditUserDescription(ct, _currentUserProvider.UserId, description);
            await _messageBus.Publish(user.ToUpdatedEvent());
            return user;
        }
    }
}