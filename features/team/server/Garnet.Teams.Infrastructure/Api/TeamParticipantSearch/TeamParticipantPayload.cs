namespace Garnet.Teams.Infrastructure.Api.TeamParticipantSearch
{
    public record TeamParticipantPayload(
        string Id,
        string UserId,
        string Username,
        string TeamId,
        string AvatarUrl
    );
}