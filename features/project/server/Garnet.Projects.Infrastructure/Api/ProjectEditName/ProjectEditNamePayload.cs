namespace Garnet.Projects.Infrastructure.Api.ProjectEditName;

public record ProjectEditNamePayload(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description,
    string? AvatarUrl,
    string[] Tags
    );