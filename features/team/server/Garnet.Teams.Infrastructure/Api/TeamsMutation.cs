using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api.TeamCreate;
using Garnet.Teams.Infrastructure.Api.TeamDelete;
using HotChocolate;
using HotChocolate.Execution;
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
            var team = await _teamService.CreateTeam(ct, input.Name, input.Description, input.Tags, new CurrentUserProvider(claims));
            return new TeamCreatePayload(team.Id, team.OwnerUserId, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamDeletePayload> TeamDelete(CancellationToken ct, ClaimsPrincipal claims, string teamId)
        {
            var result = await _teamService.DeleteTeam(ct, teamId, new CurrentUserProvider(claims));

            if (result.IsFailed)
            {
                throw new QueryException(result.Errors.Select(x=> new Error(x.Message)));
            }

            var team = result.Value;
            return new TeamDeletePayload(new TeamGet.TeamPayload(team.Id, team.Name, team.Description, team.Tags));
        }

    }
}