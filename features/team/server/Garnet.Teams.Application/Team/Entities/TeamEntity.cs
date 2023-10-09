namespace Garnet.Teams.Application
{
    public record TeamEntity(
        string Id,
        string Name,
        string Description,
        string OwnerUserId,
        string[] Tags
    );
}