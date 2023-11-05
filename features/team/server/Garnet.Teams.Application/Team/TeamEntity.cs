using Garnet.Common.Application;
using Garnet.Teams.Events.Team;

namespace Garnet.Teams.Application.Team
{
    public record TeamEntity(
        string Id,
        string Name,
        string Description,
        string OwnerUserId,
        string? AvatarUrl,
        string[] Tags,
        AuditInfo AuditInfo,
        string[] ProjectIds,
        int ParticipantCount,
        string[]? ParticipantsAvatarUrls
    );

    public static class TeamEntityExtensions
    {
        public static TeamCreatedEvent ToCreatedEvent(this TeamEntity entity)
        {
            return new TeamCreatedEvent(entity.Id, entity.Name, entity.OwnerUserId, entity.Description, entity.AvatarUrl, entity.Tags);
        }

        public static TeamDeletedEvent ToDeletedEvent(this TeamEntity entity)
        {
            return new TeamDeletedEvent(entity.Id, entity.Name, entity.OwnerUserId, entity.Description, entity.AvatarUrl, entity.Tags);
        }

        public static TeamUpdatedEvent ToUpdatedEvent(this TeamEntity entity)
        {
            return new TeamUpdatedEvent(entity.Id, entity.Name, entity.OwnerUserId, entity.Description, entity.AvatarUrl, entity.Tags);
        }
    }
}