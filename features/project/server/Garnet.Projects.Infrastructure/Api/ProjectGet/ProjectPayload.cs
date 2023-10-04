namespace Garnet.Projects.Infrastructure.Api.ProjectGet;

public record ProjectPayload(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description,
    string[] Tags
    );