namespace Garnet.Teams.Application.TeamParticipant
{
    public record TeamParticipantEntity(
        string Id,
        string UserId,
        string Username,
        string TeamId
    );
}