namespace Garnet.Teams.Infrastructure.Api.TeamGet
{
    public record TeamPayload(
        string Id,
        string Name,
        string Description,
        string AvatarUrl,
        string[] Tags
    );
}