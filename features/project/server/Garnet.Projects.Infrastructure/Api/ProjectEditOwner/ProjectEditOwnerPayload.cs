namespace Garnet.Projects.Infrastructure.Api.ProjectEditOwner;

public record ProjectEditOwnerPayload(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description,
    string? AvatarUrl,
    string[] Tags
);
