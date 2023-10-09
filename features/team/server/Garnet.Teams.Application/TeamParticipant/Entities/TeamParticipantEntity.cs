namespace Garnet.Teams.Application.TeamParticipant.Entities
{
    public record TeamParticipantEntity(
        string Id,
        string UserId,
        string Username,
        string TeamId
    );
}