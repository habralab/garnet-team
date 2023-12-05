namespace Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestsShow
{
    public record TeamUserJoinRequestShowPayload(
       string Id,
       string UserId,
       string Username,
       string[] Tags,
       string AvatarUrl,
       string TeamId,
       DateTimeOffset CreatedAt
   );
}