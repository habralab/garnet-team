namespace Garnet.Projects.Infrastructure.Api.ProjectEditDescription;

public record ProjectEditDescriptionPayload(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description
    );