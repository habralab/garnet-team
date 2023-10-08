namespace Garnet.Teams.Events
{
    public record TeamCreatedEvent(
        string Id,
        string Name,
        string OwnerUserId,
        string Description,
        string[] Tags
    );
}