using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Args;
using Garnet.Teams.Application.Team.Queries;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamUserJoinRequest;
using Garnet.Teams.Application.TeamUserJoinRequest.Queries;
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
        private readonly TeamGetQuery _teamGetQuery;
        private readonly TeamsFilterQuery _teamsFilterQuery;
        private readonly TeamUserJoinRequestsShowQuery _teamUserJoinRequestsShowQuery;
        private readonly TeamParticipantService _participantService;

        public TeamsQuery(
            TeamGetQuery teamGetQuery,
            TeamsFilterQuery teamsFilterQuery,
            TeamUserJoinRequestsShowQuery teamUserJoinRequestsShowQuery,
            TeamUserJoinRequestService userJoinRequestService,
            TeamParticipantService participantService)
        {
            _teamGetQuery = teamGetQuery;
            _teamsFilterQuery = teamsFilterQuery;
            _teamUserJoinRequestsShowQuery = teamUserJoinRequestsShowQuery;
            _participantService = participantService;
        }

        public async Task<TeamPayload> TeamGet(CancellationToken ct, string teamId)
        {
            var result = await _teamGetQuery.Query(ct, teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamPayload(team.Id, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamsFilterPayload> TeamsFilter(CancellationToken ct, TeamsFilterInput input)
        {
            var args = new TeamFilterArgs(input.Search, input.Tags ?? Array.Empty<string>(), input.Skip, input.Take);
            var teams = await _teamsFilterQuery.Query(ct, args);

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
            var result = await _teamUserJoinRequestsShowQuery.Query(ct, new CurrentUserProvider(claims), teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var userJoinRequests = result.Value.Select(x => new TeamUserJoinRequestPayload(x.Id, x.UserId, x.TeamId));
            return new TeamUserJoinRequestsShowPayload(userJoinRequests.ToArray());
        }
    }
}