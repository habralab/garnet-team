namespace Garnet.Teams.Application.Team
{
    public record TeamEntity(
        string Id,
        string Name,
        string Description,
        string OwnerUserId,
        string[] Tags
    );
}