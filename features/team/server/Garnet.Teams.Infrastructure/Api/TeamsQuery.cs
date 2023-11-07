using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application.ProjectTeamParticipant.Queries;
using Garnet.Teams.Application.Team.Args;
using Garnet.Teams.Application.Team.Queries;
using Garnet.Teams.Application.TeamJoinInvitation.Queries;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamParticipant.Queries;
using Garnet.Teams.Application.TeamUser.Queries;
using Garnet.Teams.Application.TeamUserJoinRequest.Queries;
using Garnet.Teams.Infrastructure.Api.TeamGet;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvitationsShow;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;
using Garnet.Teams.Infrastructure.Api.TeamParticipantSearch;
using Garnet.Teams.Infrastructure.Api.TeamsFilter;
using Garnet.Teams.Infrastructure.Api.TeamsList;
using Garnet.Teams.Infrastructure.Api.TeamsListByUser;
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
        private readonly TeamUserListByIdQuery _teamUserListByIdQuery;
        private readonly TeamUserJoinRequestsShowQuery _teamUserJoinRequestsShowQuery;
        private readonly TeamParticipantListByTeamsQuery _teamParticipantListByTeamsQuery;
        private readonly TeamParticipantFilterQuery _teamParticipantFilterQuery;
        private readonly TeamJoinInvitationsShowQuery _teamJoinInvitationsShowQuery;
        private readonly TeamProjectListByTeamsQuery _teamProjectGetByTeamQuery;

        public TeamsQuery(
            TeamGetQuery teamGetQuery,
            TeamsFilterQuery teamsFilterQuery,
            TeamsListByUserQuery teamsListQuery,
            TeamParticipantListByTeamsQuery teamParticipantListByTeamsQuery,
            TeamUserListByIdQuery teamUserListByIdQuery,
            TeamProjectListByTeamsQuery teamProjectGetByTeamQuery,
            TeamJoinInvitationsShowQuery teamJoinInvitationsShowQuery,
            TeamUserJoinRequestsShowQuery teamUserJoinRequestsShowQuery,
            TeamParticipantFilterQuery teamParticipantFilterQuery)
        {
            _teamsListQuery = teamsListQuery;
            _teamJoinInvitationsShowQuery = teamJoinInvitationsShowQuery;
            _teamGetQuery = teamGetQuery;
            _teamProjectGetByTeamQuery = teamProjectGetByTeamQuery;
            _teamsFilterQuery = teamsFilterQuery;
            _teamUserListByIdQuery = teamUserListByIdQuery;
            _teamParticipantListByTeamsQuery = teamParticipantListByTeamsQuery;
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
            var payloadContent = participants.Select(x => new TeamParticipantPayload(
                x.Id,
                x.UserId,
                x.Username,
                x.TeamId,
                x.AvatarUrl)).ToArray();

            return new TeamParticipantFilterPayload(payloadContent);
        }

        public async Task<TeamUserJoinRequestsShowPayload> TeamUserJoinRequestsShow(CancellationToken ct, string teamId)
        {
            var requestsResult = await _teamUserJoinRequestsShowQuery.Query(ct, teamId);
            requestsResult.ThrowQueryExceptionIfHasErrors();
            var requests = requestsResult.Value;

            var requestedUserIds = requests.Select(x => x.UserId).ToArray();
            var usersResult = await _teamUserListByIdQuery.Query(ct, requestedUserIds);

            var userJoinRequests = requestsResult.Value.Select(x =>
            {
                var user = usersResult.First(y => x.UserId == y.Id);
                return new TeamUserJoinRequestShowPayload(
                x.Id,
                user.Id,
                user.Username,
                user.Tags,
                user.AvatarUrl,
                x.TeamId,
                x.AuditInfo.CreatedAt);
            });
            return new TeamUserJoinRequestsShowPayload(userJoinRequests.ToArray());
        }

        public async Task<TeamsListPayload> TeamsListByUser(CancellationToken ct, TeamsListInput input)
        {
            var args = new TeamsListArgs(input.Skip, input.Take);
            var teamResult = await _teamsListQuery.Query(ct, input.UserId, args);
            var teamIds = teamResult.Select(x => x.Id).ToArray();
            var projectResult = await _teamProjectGetByTeamQuery.Query(ct, teamIds);
            var participantResult = await _teamParticipantListByTeamsQuery.Query(ct, teamIds);

            var teams = teamResult.Select(x =>
            {
                var participants = participantResult[x.Id].Select(x => new TeamParticipantPayload(
                    x.Id,
                    x.UserId,
                    x.Username,
                    x.TeamId,
                    x.AvatarUrl)).ToArray();

                return new TeamByUserPayload(
                 x.Id,
                 x.Name,
                 x.Description,
                 x.AvatarUrl,
                 x.Tags,
                 x.OwnerUserId,
                 projectResult.Count(c => c.Key == x.Id),
                 participants);
            });
            return new TeamsListPayload(teams.ToArray());
        }

        public async Task<TeamJoinInvitationsShowPayload> TeamJoinInvitationsShow(CancellationToken ct, string teamId)
        {
            var inviteResult = await _teamJoinInvitationsShowQuery.Query(ct, teamId);
            inviteResult.ThrowQueryExceptionIfHasErrors();
            var invite = inviteResult.Value;

            var invitedUserIds = invite.Select(x => x.UserId).ToArray();
            var usersResult = await _teamUserListByIdQuery.Query(ct, invitedUserIds);

            var joinInvitations = inviteResult.Value.Select(x =>
            {
                var user = usersResult.First(y => x.UserId == y.Id);
                return new TeamJoinInvitationShowPayload(
                x.Id,
                user.Id,
                user.Username,
                user.Tags,
                user.AvatarUrl,
                x.TeamId,
                x.AuditInfo.CreatedAt);
            });
            return new TeamJoinInvitationsShowPayload(joinInvitations.ToArray());
        }
    }
}