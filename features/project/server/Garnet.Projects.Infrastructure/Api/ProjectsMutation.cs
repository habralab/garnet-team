using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.Api.ProjectCreate;
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

    public async Task<ProjectCreatePayload> ProjectCreate(CancellationToken ct, ClaimsPrincipal claims, ProjectCreateInput input)
    {
        var result = await _projectsService.CreateProject(ct, new CurrentUserProvider(claims), input.ProjectName, input.Description);
        return new ProjectCreatePayload(result.Id, result.OwnerUserId, result.ProjectName, result.Description);
    }
}