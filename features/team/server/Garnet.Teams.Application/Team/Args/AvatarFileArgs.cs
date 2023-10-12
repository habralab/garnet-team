namespace Garnet.Teams.Application.Team.Args
{
    public record AvatarFileArgs(
        string Filename,
        string? ContentType,
        Stream Stream
    );
}