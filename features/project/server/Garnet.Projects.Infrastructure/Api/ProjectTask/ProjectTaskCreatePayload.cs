namespace Garnet.Projects.Infrastructure.Api.ProjectTask;

public record ProjectTaskCreatePayload(
    string Id,
    int TaskNumber,
    string ProjectId,
    string ResponsibleUserId,
    string Name,
    string? Description,
    string Status,
    string[] TeamExecutorIds,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels
);