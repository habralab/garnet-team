namespace Garnet.Projects.Infrastructure.Api.ProjectCreate;

public record ProjectCreatePayload(string Id, string OwnerUserId, string ProjectName, string? Description, string[] Tags);