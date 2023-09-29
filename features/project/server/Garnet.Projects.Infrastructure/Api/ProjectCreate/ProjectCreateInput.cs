namespace Garnet.Projects.Infrastructure.Api.ProjectCreate;

public record ProjectCreateInput(string ProjectName, string? Description = null);