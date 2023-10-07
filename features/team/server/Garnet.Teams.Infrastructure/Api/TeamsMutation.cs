using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api.TeamCreate;
using Garnet.Teams.Infrastructure.Api.TeamDelete;
using Garnet.Teams.Infrastructure.Api.TeamEditDescription;
using Garnet.Teams.Infrastructure.Api.TeamEditOwner;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestApprove;
using HotChocolate.Types;

namespace Garnet.Teams.Infrastructure.Api
{
    [ExtendObjectType("Mutation")]
    public class TeamsMutation
    {
        private readonly TeamService _teamService;
        private readonly TeamUserJoinRequestService _userJoinRequestService;

        public TeamsMutation(TeamService teamService, TeamUserJoinRequestService userJoinRequestService)
        {
            _teamService = teamService;
            _userJoinRequestService = userJoinRequestService;
        }

        public async Task<TeamCreatePayload> TeamCreate(CancellationToken ct, ClaimsPrincipal claims, TeamCreateInput input)
        {
            var result = await _teamService.CreateTeam(ct, input.Name, input.Description, input.Tags, new CurrentUserProvider(claims));
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamCreatePayload(team.Id, team.OwnerUserId, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamDeletePayload> TeamDelete(CancellationToken ct, ClaimsPrincipal claims, string teamId)
        {
            var result = await _teamService.DeleteTeam(ct, teamId, new CurrentUserProvider(claims));
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamDeletePayload(team.Id, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamEditDescriptionPayload> TeamEditDescription(CancellationToken ct, ClaimsPrincipal claims, TeamEditDescriptionInput input)
        {
            var result = await _teamService.EditTeamDescription(ct, input.Id, input.Description, new CurrentUserProvider(claims));
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamEditDescriptionPayload(team.Id, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamEditOwnerPayload> TeamEditOwner(CancellationToken ct, ClaimsPrincipal claims, TeamEditOwnerInput input)
        {
            var result = await _teamService.EditTeamOwner(ct, input.TeamId, input.NewOwnerUserId, new CurrentUserProvider(claims));
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamEditOwnerPayload(team.Id, team.Name, team.Description, team.Tags, team.OwnerUserId);
        }

        public async Task<TeamUserJoinRequestPayload> TeamUserJoinRequestCreate(CancellationToken ct, ClaimsPrincipal claims, string teamId)
        {
            var result = await _userJoinRequestService.CreateJoinRequestByUser(ct, teamId, new CurrentUserProvider(claims));
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamUserJoinRequestPayload(team.Id, team.UserId, team.TeamId);
        }

        public async Task<TeamUserJoinRequestPayload> TeamUserJoinRequestDecide(CancellationToken ct, ClaimsPrincipal claims, TeamUserJoinRequestDecideInput input)
        {
            var result = await _userJoinRequestService.UserJoinRequestDecide(ct, new CurrentUserProvider(claims), input.UserJoinRequestId, input.IsApproved);
            result.ThrowQueryExceptionIfHasErrors();

            var userJoinRequest = result.Value;
            return new TeamUserJoinRequestPayload(userJoinRequest.Id, userJoinRequest.UserId, userJoinRequest.TeamId);
        }
    }
}