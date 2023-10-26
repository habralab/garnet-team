namespace Garnet.Projects.Application.Project.Args;

public record AvatarFileArgs(
    string Filename,
    string? ContentType,
    Stream Stream
);