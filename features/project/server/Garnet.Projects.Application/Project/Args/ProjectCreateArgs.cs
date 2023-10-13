namespace Garnet.Projects.Application.Args;

public record ProjectCreateArgs(
    string ProjectName,
    string OwnerUserId,
    string? Description,
    string? AvatarUrl,
    string[] Tags
    );