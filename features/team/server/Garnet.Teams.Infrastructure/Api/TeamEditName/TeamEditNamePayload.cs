using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.Infrastructure.Api.TeamEditName
{
    public record TeamEditNamePayload(
        string Id,
        string Name,
        string Description,
        string? AvatarUrl,
        string[] Tags
    ) : TeamPayload(Id, Name, Description, AvatarUrl, Tags);
}