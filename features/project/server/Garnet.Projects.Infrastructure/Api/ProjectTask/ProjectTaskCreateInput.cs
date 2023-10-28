namespace Garnet.Projects.Infrastructure.Api.ProjectTask;

public record ProjectTaskCreateInput(
    string ProjectId,
    string Name,
    string? Description,
    string[] TeamExecutorIds,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels
);