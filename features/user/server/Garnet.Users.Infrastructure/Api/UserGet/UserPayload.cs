using HotChocolate.Authorization;

namespace Garnet.Users.Infrastructure.Api.UserGet;

[Authorize]
public record UserPayload(
    string Id,
    string UserName,
    string Description,
    string AvatarUrl,
    string[] Tags
);