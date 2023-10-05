using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api.TeamGet;
using Garnet.Teams.Infrastructure.Api.TeamParticipantSearch;
using Garnet.Teams.Infrastructure.Api.TeamsFilter;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestsShow;
using HotChocolate.Types;

namespace Garnet.Teams.Infrastructure.Api
{
    [ExtendObjectType("Query")]

    public class TeamsQuery
    {
        private readonly TeamService _teamService;
        private readonly TeamParticipantService _participantService;
        private readonly TeamMembershipService _membershipService;

        public TeamsQuery(
            TeamService teamService,
            TeamMembershipService membershipService,
            TeamParticipantService participantService)
        {
            _teamService = teamService;
            _membershipService = membershipService;
            _participantService = participantService;
        }

        public async Task<TeamPayload> TeamGet(CancellationToken ct, string teamId)
        {
            var result = await _teamService.GetTeamById(ct, teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamPayload(team.Id, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamsFilterPayload> TeamsFilter(CancellationToken ct, TeamsFilterInput input)
        {
            var teams = await _teamService.FilterTeams(
                ct,
                input.Search,
                input.Tags ?? Array.Empty<string>(),
                input.Skip,
                input.Take);

            return new TeamsFilterPayload(teams.Select(x => new TeamPayload(x.Id, x.Name, x.Description, x.Tags)).ToArray());
        }

        public async Task<TeamParticipantFilterPayload> TeamParticipantFilter(CancellationToken ct, TeamParticipantFilterInput input)
        {
            var participants = await _participantService.FindTeamParticipantByUsername(ct, input.TeamId, input.Search, input.Take, input.Skip);
            var payloadContent = participants.Select(x => new TeamParticipantPayload(x.Id, x.UserId, x.TeamId)).ToArray();

            return new TeamParticipantFilterPayload(payloadContent);
        }

        public async Task<TeamUserJoinRequestsShowPayload> TeamUserJoinRequestsShow(CancellationToken ct, ClaimsPrincipal claims, string teamId)
        {
            var result = await _membershipService.GetAllUserJoinRequestByTeam(ct, new CurrentUserProvider(claims), teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var userJoinRequests = result.Value.Select(x => new TeamUserJoinRequestPayload(x.Id, x.UserId, x.TeamId));
            return new TeamUserJoinRequestsShowPayload(userJoinRequests.ToArray());
        }
    }
}