namespace Garnet.Projects.Infrastructure.Api.ProjectTask;

public record ProjectTaskCreatePayload(
    string Id,
    string ProjectId,
    string UserCreatorId,
    string Name,
    string? Description,
    string Status,
    string? TeamExecutorId,
    string? UserExecutorId,
    string[] Tags,
    string[] Labels
);