namespace Garnet.Projects.Infrastructure.Api.ProjectTaskCreate;

public record ProjectTaskCreateInput(
    string ProjectId,
    string Name,
    string? Description,
    string[] TeamExecutorIds,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels
);