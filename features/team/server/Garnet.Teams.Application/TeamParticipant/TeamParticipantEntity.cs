using Garnet.Teams.Events.TeamParticipant;

namespace Garnet.Teams.Application.TeamParticipant
{
    public record TeamParticipantEntity(
        string Id,
        string UserId,
        string Username,
        string TeamId
    );

    public static class TeamParticipantEntityExtension
    {
        public static TeamParticipantLeftTeamEvent ToLeftTeamEvent(this TeamParticipantEntity entity)
        {
            return new TeamParticipantLeftTeamEvent(entity.Id, entity.UserId, entity.TeamId);
        }
    }
}