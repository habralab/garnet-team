namespace Garnet.Teams.Application
{
    public record TeamParticipantEntity(
        string Id,
        string UserId,
        string Username,
        string TeamId
    );
}