namespace Garnet.Teams.Application.TeamUser
{
    public record TeamUserEntity(
        string Id,
        string Username,
        string[] Tags,
        string AvatarUrl
    );
}