namespace Garnet.Projects.Infrastructure.Api.ProjectGet;

public record ProjectPayload(
    string OwnerUserId,
    string ProjectName,
    string? Description
    );