namespace Garnet.Teams.Events.Team
{
    public record TeamUpdatedEvent(
        string Id,
        string Name,
        string OwnerUserId,
        string Description,
        string[] Tags
    );
}