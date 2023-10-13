using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.Infrastructure.Api.TeamCreate
{
    public record TeamCreatePayload(string Id,
        string Name,
        string Description,
        string? AvatarUrl,
        string OwnerUserId,
        string[] Tags
        ) : TeamPayload(Id, Name, Description, AvatarUrl, Tags, OwnerUserId);
}