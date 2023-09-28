namespace Garnet.Teams.Application
{
    public record Team(
        string Id,
        string Name,
        string Description,
        string OwnerUserId
    );
}