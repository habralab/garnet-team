using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Common.Application.S3;
using Garnet.Users.Application.Errors;

namespace Garnet.Users.Application.Commands
{
    public class UserUploadAvatarCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IUsersRepository _usersRepository;
        private readonly IMessageBus _messageBus;
        private readonly IRemoteFileStorage _fileStorage;


        public UserUploadAvatarCommand(
            ICurrentUserProvider currentUserProvider,
            IUsersRepository usersRepository,
            IRemoteFileStorage fileStorage,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _usersRepository = usersRepository;
            _messageBus = messageBus;
            _fileStorage = fileStorage;
        }

        public async Task<Result<User>> Execute(
            string fileName,
            string? contentType,
            Stream imageStream)
        {
            var user = await _usersRepository.GetUser(_currentUserProvider.UserId);
            if (user is null)
            {
                return Result.Fail(new UserNotFoundError(_currentUserProvider.UserId));
            }

            var avatarLink = await _fileStorage.UploadFile($"avatars/{_currentUserProvider.UserId}", contentType, imageStream);

            user = await _usersRepository.EditUserAvatar(_currentUserProvider.UserId, avatarLink);
            await _messageBus.Publish(user.ToUpdatedEvent());
            return user;
        }
    }
}