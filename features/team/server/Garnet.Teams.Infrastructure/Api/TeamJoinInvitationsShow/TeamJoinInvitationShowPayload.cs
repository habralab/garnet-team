using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;

namespace Garnet.Teams.Infrastructure.Api.TeamJoinInvitationsShow
{
    public record TeamJoinInvitationShowPayload(TeamJoinInvitePayload[] TeamJoinInvites);
}