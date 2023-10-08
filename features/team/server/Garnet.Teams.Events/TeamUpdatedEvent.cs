namespace Garnet.Teams.Events
{
    public record TeamUpdatedEvent(
        string Id,
        string Name,
        string OwnerUserId,
        string Description,
        string[] Tags
    );
}