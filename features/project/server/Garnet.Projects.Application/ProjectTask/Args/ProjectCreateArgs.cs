﻿namespace Garnet.Projects.Application.ProjectTask.Args;

public record ProjectTaskCreateArgs(
    string ProjectId,
    string Name,
    string? Description,
    string Status,
    string? TeamExecutorId,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels
    );