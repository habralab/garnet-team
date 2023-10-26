using Garnet.Users.Infrastructure.Api.UserGet;

namespace Garnet.Users.Infrastructure.Api.UserEdit.UserEditTags
{
    public record UserEditTagsPayload(
        string Id,
        string UserName,
        string Description,
        string AvatarUrl,
        string[] Tags
        ) : UserPayload(Id, UserName, Description, AvatarUrl, Tags);
}