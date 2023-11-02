namespace Garnet.Teams.Application.TeamUser.Args
{
    public record TeamUserCreateArgs(
        string UserId,
        string Username,
        string? AvatarUrl
    );
}