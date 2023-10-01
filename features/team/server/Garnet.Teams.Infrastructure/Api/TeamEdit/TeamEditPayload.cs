using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.Infrastructure.Api.TeamEdit
{
    public record TeamEditPayload(
        string Id,
        string Description
    );
}