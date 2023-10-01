using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api.TeamCreate;
using Garnet.Teams.Infrastructure.Api.TeamDelete;
using Garnet.Teams.Infrastructure.Api.TeamEdit;
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
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamDeletePayload(new TeamGet.TeamPayload(team.Id, team.Name, team.Description, team.Tags));
        }

        public async Task<TeamEditPayload> TeamEdit(CancellationToken ct, ClaimsPrincipal claims, TeamEditInput input)
        {
            var result = await _teamService.EditTeam(ct, input.Id, input.Description, new CurrentUserProvider(claims));
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamEditPayload(new TeamGet.TeamPayload(team.Id, team.Name, team.Description, team.Tags));
        }
    }
}