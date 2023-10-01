using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.Infrastructure.Api.TeamEditDescription
{
    public record TeamEditDescriptionPayload(
        string Id,
        string Name,
        string Description,
        string[] Tags
    ) : TeamPayload(Id, Name, Description, Tags);
}