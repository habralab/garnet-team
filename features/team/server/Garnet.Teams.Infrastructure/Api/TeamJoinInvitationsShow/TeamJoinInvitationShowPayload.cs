namespace Garnet.Teams.Infrastructure.Api.TeamJoinInvitationsShow
{
    public record TeamJoinInvitationShowPayload(
       string Id,
       string UserId,
       string Username,
       string[] Tags,
       string AvatarUrl,
       string TeamId,
       DateTimeOffset CreatedAt
   );
}