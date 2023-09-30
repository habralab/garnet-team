using Garnet.Users.Infrastructure.Api.UserGet;

namespace Garnet.Users.Infrastructure.Api.UserCreate;

public record UserCreatePayload(string Id, string UserName, string Description, string AvatarUrl, string[] Tags) 
    : UserPayload(Id, UserName, Description, AvatarUrl, Tags);