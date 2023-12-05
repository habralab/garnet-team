namespace Garnet.Teams.Application.TeamUser.Args
{
    public record TeamUserUpdateArgs(
        string Username,
        string[] Tags,
        string AvatarUrl
    );
}