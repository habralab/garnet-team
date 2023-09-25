using Garnet.Common.Application;

namespace Garnet.Users.Application;

public class UsersService
{
    private readonly IUsersRepository _repository;

    public UsersService(
        IUsersRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<User> CreateSystemUser(CancellationToken ct)
    {
        return await _repository.CreateSystemUser(ct);
    }

    public async Task<User> CreateUser(CancellationToken ct, string identityId, string username)
    {
        return await _repository.CreateUser(ct, identityId, username);
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
        return await _repository.EditUserDescription(ct, currentUserProvider.UserId, description);
    }
}