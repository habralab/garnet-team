namespace Garnet.Teams.Application
{
    public record TeamParticipant(
        string Id,
        string UserId,
        string Username,
        string TeamId
    );
}