namespace Garnet.Projects.Infrastructure.Api.ProjectUploadAvatar;

public record ProjectUploadAvatarPayload(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description,
    string? AvatarUrl,
    string[] Tags
    );