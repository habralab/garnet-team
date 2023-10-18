using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application.Team.Args;
using Garnet.Teams.Application.Team.Queries;
using Garnet.Teams.Application.TeamJoinInvitation.Queries;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamParticipant.Queries;
using Garnet.Teams.Application.TeamUserJoinRequest.Queries;
using Garnet.Teams.Infrastructure.Api.TeamGet;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvitationsShow;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;
using Garnet.Teams.Infrastructure.Api.TeamParticipantSearch;
using Garnet.Teams.Infrastructure.Api.TeamsFilter;
using Garnet.Teams.Infrastructure.Api.TeamsList;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestsShow;
using HotChocolate.Types;

namespace Garnet.Teams.Infrastructure.Api
{
    [ExtendObjectType("Query")]

    public class TeamsQuery
    {
        private readonly TeamGetQuery _teamGetQuery;
        private readonly TeamsListByUserQuery _teamsListQuery;
        private readonly TeamsFilterQuery _teamsFilterQuery;
        private readonly TeamUserJoinRequestsShowQuery _teamUserJoinRequestsShowQuery;
        private readonly TeamParticipantFilterQuery _teamParticipantFilterQuery;
        private readonly TeamJoinInvitationsShowQuery _teamJoinInvitationsShowQuery;

        public TeamsQuery(
            TeamGetQuery teamGetQuery,
            TeamsFilterQuery teamsFilterQuery,
            TeamsListByUserQuery teamsListQuery,
            TeamJoinInvitationsShowQuery teamJoinInvitationsShowQuery,
            TeamUserJoinRequestsShowQuery teamUserJoinRequestsShowQuery,
            TeamParticipantFilterQuery teamParticipantFilterQuery)
        {
            _teamsListQuery = teamsListQuery;
            _teamJoinInvitationsShowQuery = teamJoinInvitationsShowQuery;
            _teamGetQuery = teamGetQuery;
            _teamsFilterQuery = teamsFilterQuery;
            _teamUserJoinRequestsShowQuery = teamUserJoinRequestsShowQuery;
            _teamParticipantFilterQuery = teamParticipantFilterQuery;
        }

        public async Task<TeamPayload> TeamGet(CancellationToken ct, string teamId)
        {
            var result = await _teamGetQuery.Query(ct, teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamPayload(team.Id, team.Name, team.Description, team.AvatarUrl, team.Tags, team.OwnerUserId);
        }

        public async Task<TeamsFilterPayload> TeamsFilter(CancellationToken ct, TeamsFilterInput input)
        {
            var args = new TeamFilterArgs(input.Search, input.Tags ?? Array.Empty<string>(), input.Skip, input.Take);
            var result = await _teamsFilterQuery.Query(ct, args);
            var teams = result.Select(x => new TeamPayload(x.Id, x.Name, x.Description, x.AvatarUrl, x.Tags, x.OwnerUserId));

            return new TeamsFilterPayload(teams.ToArray());
        }

        public async Task<TeamParticipantFilterPayload> TeamParticipantFilter(CancellationToken ct, TeamParticipantFilterInput input)
        {
            var args = new TeamParticipantFilterArgs(input.Search, input.TeamId, input.Skip, input.Take);
            var participants = await _teamParticipantFilterQuery.Query(ct, args);
            var payloadContent = participants.Select(x => new TeamParticipantPayload(x.Id, x.UserId, x.TeamId)).ToArray();

            return new TeamParticipantFilterPayload(payloadContent);
        }

        public async Task<TeamUserJoinRequestsShowPayload> TeamUserJoinRequestsShow(CancellationToken ct, string teamId)
        {
            var result = await _teamUserJoinRequestsShowQuery.Query(ct, teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var userJoinRequests = result.Value.Select(x => new TeamUserJoinRequestPayload(x.Id, x.UserId, x.TeamId));
            return new TeamUserJoinRequestsShowPayload(userJoinRequests.ToArray());
        }

        public async Task<TeamsListPayload> TeamsListByUser(CancellationToken ct, TeamsListInput input)
        {
            var args = new TeamsListArgs(input.Skip, input.Take);
            var result = await _teamsListQuery.Query(ct, input.UserId, args);
            var teams = result.Select(x => new TeamPayload(x.Id, x.Name, x.Description, x.AvatarUrl, x.Tags, x.OwnerUserId));

            return new TeamsListPayload(teams.ToArray());
        }

        public async Task<TeamJoinInvitationShowPayload> TeamJoinInvitationsShow(CancellationToken ct, string teamId)
        {
            var result = await _teamJoinInvitationsShowQuery.Query(ct, teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var joinInvitations = result.Value.Select(x => new TeamJoinInvitePayload(x.Id, x.UserId, x.TeamId, x.AuditInfo.CreatedAt));
            return new TeamJoinInvitationShowPayload(joinInvitations.ToArray());
        }
    }
}