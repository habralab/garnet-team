using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.Infrastructure.Api.TeamEditOwner
{
    public record TeamEditOwnerPayload(
        string Id,
        string Name,
        string Description,
        string? AvatarUrl,
        string[] Tags,
        string OwnerUserId
    ) : TeamPayload(Id, Name, Description, AvatarUrl, Tags);
}