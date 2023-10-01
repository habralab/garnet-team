using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.Infrastructure.Api.TeamDelete
{
    public record TeamDeletePayload(
        string Id,
        string Name,
        string Description,
        string[] Tags
    ) : TeamPayload(Id, Name, Description, Tags);
}