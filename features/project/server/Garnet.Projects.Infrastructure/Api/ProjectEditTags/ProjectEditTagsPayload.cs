namespace Garnet.Projects.Infrastructure.Api.ProjectEditTags;

public record ProjectEditTagsPayload(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description,
    string? AvatarUrl,
    string[] Tags
);