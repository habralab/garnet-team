namespace Garnet.Projects.Infrastructure.Api.ProjectTask;

public record ProjectTaskCreateInput(
    string ProjectId,
    string Name,
    string? Description,
    string? TeamExecutorId,
    string? UserExecutorId,
    string[] Tags
);