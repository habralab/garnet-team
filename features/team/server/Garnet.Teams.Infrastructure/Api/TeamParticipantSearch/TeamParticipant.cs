namespace Garnet.Teams.Infrastructure.Api.TeamParticipantSearch
{
    public record TeamParticipant(
        string Id,
        string UserId,
        string TeamId
    );
}