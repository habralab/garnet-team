using Garnet.Users.Application.Args;

namespace Garnet.Users.Application;

public interface IUsersRepository
{
    Task<User?> GetUser(CancellationToken ct, string id);
    Task<User> EditUserDescription(CancellationToken ct, string userId, string description);
    Task<User> EditUserTags(CancellationToken ct, string userId, string[] tags);
    Task<User> EditUserAvatar(CancellationToken ct, string userId, string avatarUrl);
    Task<User> CreateUser(CancellationToken ct, string identityId, string username);
    Task<User[]> FilterUsers(CancellationToken ct, UserFilterArgs args);
    Task CreateIndexes(CancellationToken ct);
}