using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.Api.ProjectGet;
using HotChocolate.Execution;
using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api;

[ExtendObjectType("Query")]
public class ProjectsQuery
{
    private readonly ProjectsService _projectsService;

    public ProjectsQuery(ProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    public async Task<ProjectPayload> ProjectGet(CancellationToken ct, string projectId)
    {
        var project = await _projectsService.GetProject(ct, projectId)
                      ?? throw new QueryException($"Проект с идентификатором '{projectId}' не найден");
        return new ProjectPayload(project.OwnerUserId, project.ProjectName, project.Description);
    }
}