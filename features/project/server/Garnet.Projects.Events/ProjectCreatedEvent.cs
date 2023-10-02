﻿namespace Garnet.Projects.Events;

public record ProjectCreatedEvent(
    string ProjectId,
    string ProjectName,
    string OwnerUserId,
    string? Description
);
