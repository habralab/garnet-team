namespace Garnet.Teams.Events.Team
{
    public record TeamDeletedEvent(
        string Id,
        string Name,
        string OwnerUserId,
        string Description,
        string? AvatarUrl,
        string[] Tags
    );
}