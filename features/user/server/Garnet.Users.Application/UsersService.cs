using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Users.Events;

namespace Garnet.Users.Application;

public class UsersService
{
    private readonly IUsersRepository _repository;
    private readonly IMessageBus _messageBus;

    public UsersService(
        IUsersRepository repository,
        IMessageBus messageBus
    )
    {
        _repository = repository;
        _messageBus = messageBus;
    }

    public async Task<User> CreateSystemUser(CancellationToken ct)
    {
        return await _repository.CreateSystemUser(ct);
    }

    public async Task<User> CreateUser(CancellationToken ct, string identityId, string username)
    {
        var user = await _repository.CreateUser(ct, identityId, username);
        await _messageBus.Publish(new UserCreatedEvent(user.Id, user.UserName));
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
    
    public async Task<User> EditCurrentUserDescription(CancellationToken ct, ICurrentUserProvider currentUserProvider, string description)
    {
        var user = await _repository.EditUserDescription(ct, currentUserProvider.UserId, description);
        await _messageBus.Publish(new UserUpdatedEvent(user.Id, user.UserName, user.Description, user.Tags));
        return user;
    }
}