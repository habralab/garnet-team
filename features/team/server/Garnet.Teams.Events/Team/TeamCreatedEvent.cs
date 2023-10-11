namespace Garnet.Teams.Events.Team
{
    public record TeamCreatedEvent(
        string Id,
        string Name,
        string OwnerUserId,
        string Description,
        string AvatarUrl,
        string[] Tags
    );
}