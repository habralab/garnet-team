using Garnet.Users.Infrastructure.Api.UserGet;

namespace Garnet.Users.Infrastructure.Api.UserEdit;

public record UserEditDescriptionPayload(string Id, string UserName, string Description, string[] Tags) 
    : UserPayload(Id, UserName, Description, Tags);