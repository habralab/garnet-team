using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.Api.ProjectGet;
using HotChocolate.Execution;
using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api;

[ExtendObjectType("Query")]
public class ProjectsQuery
{
    private readonly ProjectService _projectService;

    public ProjectsQuery(ProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<ProjectPayload> ProjectGet(CancellationToken ct, string projectId)
    {
        var result = await _projectService.GetProject(ct, projectId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectPayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description);
    }
}