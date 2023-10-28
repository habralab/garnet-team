﻿namespace Garnet.Projects.Events.ProjectTask;

public record ProjectTaskUpdatedEvent(
    string Id,
    int TaskNumber,
    string ProjectId,
    string ResponsibleUserId,
    string Name,
    string? Description,
    string Status,
    string? TeamExecutorId,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels
);