﻿namespace Garnet.Projects.Events.Project;

public record ProjectCreatedEvent(
    string ProjectId,
    string ProjectName,
    string OwnerUserId,
    string? Description,
    string? AvatarUrl,
    string[] Tags,
    int TasksCounter
);
