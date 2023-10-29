namespace Garnet.Projects.Infrastructure.Api.ProjectTaskGet;

public record ProjectTaskGetPayload(
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