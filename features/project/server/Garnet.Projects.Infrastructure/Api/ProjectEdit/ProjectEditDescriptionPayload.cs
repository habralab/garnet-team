namespace Garnet.Projects.Infrastructure.Api.ProjectEdit;

public record ProjectEditDescriptionPayload(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description
    );