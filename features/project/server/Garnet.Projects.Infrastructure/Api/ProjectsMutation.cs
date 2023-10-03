using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.Api.ProjectCreate;
using Garnet.Projects.Infrastructure.Api.ProjectDelete;
using Garnet.Projects.Infrastructure.Api.ProjectGet;
using Garnet.Projects.Infrastructure.Api.ProjectEdit;
using Garnet.Projects.Infrastructure.Api.ProjectEditOwner;
using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api;

[ExtendObjectType("Mutation")]
public class ProjectsMutation
{
    private readonly ProjectsService _projectsService;

    public ProjectsMutation(ProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    public async Task<ProjectCreatePayload> ProjectCreate(CancellationToken ct, ClaimsPrincipal claims,
        ProjectCreateInput input)
    {
        var result = await _projectsService.CreateProject(ct, new CurrentUserProvider(claims), input.ProjectName,
            input.Description);
        return new ProjectCreatePayload(result.Id, result.OwnerUserId, result.ProjectName, result.Description);
    }

    public async Task<ProjectEditDescriptionPayload> ProjectEditDescription(CancellationToken ct, ClaimsPrincipal claims, ProjectEditDescriptionInput input)
    {
        var result = await _projectsService.EditProjectDescription(ct, new CurrentUserProvider(claims), input.ProjectId, input.Description);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditDescriptionPayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description);
    }

    public async Task<ProjectDeletePayload?> ProjectDelete(CancellationToken ct, ClaimsPrincipal claims,
        string projectId)
    {
        var result = await _projectsService.DeleteProject(ct, new CurrentUserProvider(claims), projectId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectDeletePayload(project.Id, project.OwnerUserId, project.ProjectName,
            project.Description);
    }

    public async Task<ProjectEditOwnerPayload> ProjectEditOwner(CancellationToken ct, ClaimsPrincipal claims, ProjectEditOwnerInput input)
    {
        var result = await _projectsService.EditProjectOwner(ct, new CurrentUserProvider(claims), input.ProjectId, input.NewOwnerUserId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditOwnerPayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description);
    }
}