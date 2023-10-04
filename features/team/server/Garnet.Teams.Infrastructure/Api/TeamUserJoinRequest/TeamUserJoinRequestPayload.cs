namespace Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest
{
    public record TeamUserJoinRequestCreatePayload(
        string Id,
        string UserId,
        string TeamId
    );
}