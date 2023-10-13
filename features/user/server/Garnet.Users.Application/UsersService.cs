using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Common.Application.S3;

namespace Garnet.Users.Application;

public class UsersService
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUsersRepository _repository;
    private readonly IMessageBus _messageBus;
    private readonly IRemoteFileStorage _fileStorage;

    public UsersService(
        ICurrentUserProvider currentUserProvider,
        IUsersRepository repository,
        IMessageBus messageBus,
        IRemoteFileStorage fileStorage
    )
    {
        _currentUserProvider = currentUserProvider;
        _repository = repository;
        _messageBus = messageBus;
        _fileStorage = fileStorage;
    }

    public async Task<User> CreateUser(CancellationToken ct, string identityId, string username)
    {
        var user = await _repository.CreateUser(ct, identityId, username);
        await _messageBus.Publish(user.ToCreatedEvent());
        return user;
    }
    
    public async Task<User?> GetUser(CancellationToken ct, string id)
    {
        return await _repository.GetUser(ct, id);
    }
    
    public async Task<User[]> FilterUsers(CancellationToken ct, string? search, string[] tags, int skip, int take)
    {
        return await _repository.FilterUsers(ct, search, tags, skip, take);
    }
    
    public async Task<User> EditCurrentUserDescription(CancellationToken ct, string description)
    {
        var user = await _repository.EditUserDescription(ct, _currentUserProvider.UserId, description);
        await _messageBus.Publish(user.ToUpdatedEvent());
        return user;
    }

    public async Task<User> EditCurrentUserAvatar(
        CancellationToken ct,
        string fileName,
        string? contentType,
        Stream imageStream)
    {
        var avatarLink = await _fileStorage.UploadFile($"avatars/{_currentUserProvider.UserId}", contentType, imageStream);
        var user = await _repository.EditUserAvatar(ct, _currentUserProvider.UserId, avatarLink);
        await _messageBus.Publish(user.ToUpdatedEvent());
        return user;
    }
}