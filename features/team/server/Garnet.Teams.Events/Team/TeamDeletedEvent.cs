namespace Garnet.Teams.Events
{
    public record TeamDeletedEvent(
        string Id,
        string Name,
        string OwnerUserId,
        string Description,
        string[] Tags
    );
}