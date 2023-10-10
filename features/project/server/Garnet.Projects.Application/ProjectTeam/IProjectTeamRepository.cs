﻿namespace Garnet.Projects.Application.ProjectTeam;

public interface IProjectTeamRepository
{
    Task<ProjectTeamEntity> AddProjectTeam(CancellationToken ct, string teamId, string teamName,
        string ownerUserId);

    Task<ProjectTeamEntity> UpdateProjectTeam(CancellationToken ct, string teamId, string teamName,
        string ownerUserId);
}