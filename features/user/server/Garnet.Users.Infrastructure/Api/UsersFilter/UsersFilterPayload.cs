using Garnet.Users.Infrastructure.Api.UserGet;

namespace Garnet.Users.Infrastructure.Api.UsersFilter;

public record UsersFilterPayload(UserPayload[] Users);