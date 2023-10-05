using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest;

namespace Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestsShow
{
    public record TeamUserJoinRequestsShowPayload(TeamUserJoinRequestCreatePayload[] TeamUserJoinRequests);
}