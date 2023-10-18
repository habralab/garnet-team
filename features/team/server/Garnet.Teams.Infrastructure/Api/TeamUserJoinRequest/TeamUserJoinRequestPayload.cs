namespace Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest
{
    public record TeamUserJoinRequestPayload(
        string Id,
        string UserId,
        string TeamId,
        DateTimeOffset CreatedAt
    );
}