namespace Garnet.Teams.Application.Team.Args
{
    public record TeamCreateArgs(
        string Name,
        string Description,
        AvatarFileArgs? Avatar,
        string[] Tags
    );
}