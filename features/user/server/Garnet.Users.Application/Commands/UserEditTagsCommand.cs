using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;

namespace Garnet.Users.Application.Commands
{
    public class UserEditTagsCommand
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMessageBus _messageBus;
        private readonly ICurrentUserProvider _currentUserProvider;

        public UserEditTagsCommand(
            ICurrentUserProvider currentUserProvider,
            IUsersRepository usersRepository,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _usersRepository = usersRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<User>> Execute(CancellationToken ct, string[] tags)
        {
            var user = await _usersRepository.GetUser(ct, _currentUserProvider.UserId);
            if (user is null)
            {
                
            }

            user = await _usersRepository.EditUserTags(ct, user.Id, tags);
            await _messageBus.Publish(user.ToUpdatedEvent());
            return Result.Ok(user);
        }
    }
}