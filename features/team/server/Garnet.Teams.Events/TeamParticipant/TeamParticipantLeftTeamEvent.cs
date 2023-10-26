namespace Garnet.Teams.Events.TeamParticipant
{
    public record TeamParticipantLeftTeamEvent(
        string Id,
        string UserId,
        string TeamId
    );
}