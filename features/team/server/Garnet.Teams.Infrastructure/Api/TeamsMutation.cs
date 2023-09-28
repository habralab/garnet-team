using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api.TeamCreate;
using HotChocolate.Types;

namespace Garnet.Teams.Infrastructure.Api
{
    [ExtendObjectType("Mutation")]
    public class TeamsMutation
    {
        private readonly TeamService _teamService;

        public TeamsMutation(TeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<TeamCreatePayload> TeamCreate(CancellationToken ct, ClaimsPrincipal claims, TeamCreateInput input)
        {
            var team = await _teamService.CreateTeam(ct, input.Name, input.Description, new CurrentUserProvider(claims));
            return new TeamCreatePayload(team.Id, team.OwnerUserId, team.Name, team.Description);
        }
    }
}