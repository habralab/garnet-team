namespace Garnet.Teams.Infrastructure.Api.TeamParticipantLeaveTeam
{
    public record TeamParticipantLeaveTeamPayload(
        string Id,
        string UserId,
        string TeamId
    );
}