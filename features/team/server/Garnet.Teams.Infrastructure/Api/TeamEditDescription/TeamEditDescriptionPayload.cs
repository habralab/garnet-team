using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.Infrastructure.Api.TeamEditDescription
{
    public record TeamEditDescriptionPayload(
        string Id,
        string Name,
        string Description,
        string? AvatarUrl,
        string[] Tags,
        string OwnerUserId
    ) : TeamPayload(Id, Name, Description, AvatarUrl, Tags, OwnerUserId);
}