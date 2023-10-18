namespace Garnet.Projects.Application.Project.Args;

public record ProjectCreateArgs(
    string ProjectName,
    string? Description,
    AvatarFileArgs? Avatar,
    string[] Tags
    );