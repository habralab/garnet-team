using System.Security.Claims;
using Garnet.Teams.Infrastructure.Api.TeamGet;
using HotChocolate.Types;

namespace Garnet.Teams.Infrastructure.Api
{
    [ExtendObjectType("Query")]

    public class TeamsQuery
    {
        public async Task<TeamGetPayload> TeamGet(CancellationToken ct, ClaimsPrincipal claims, string teamId)
        {
            return null;
        }
    }
}