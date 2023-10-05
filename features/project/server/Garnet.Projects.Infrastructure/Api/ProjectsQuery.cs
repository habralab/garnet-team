using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.Api.ProjectFilter;
using Garnet.Projects.Infrastructure.Api.ProjectGet;
using Garnet.Projects.Infrastructure.Api.ProjectTeamParticipant;
using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api;

[ExtendObjectType("Query")]
public class ProjectsQuery
{
    private readonly ProjectService _projectService;
    private readonly ProjectTeamParticipantService _projectTeamParticipantService;

    public ProjectsQuery(ProjectService projectService, ProjectTeamParticipantService projectTeamParticipantService)
    {
        _projectService = projectService;
        _projectTeamParticipantService = projectTeamParticipantService;
    }

    public async Task<ProjectPayload> ProjectGet(CancellationToken ct, string projectId)
    {
        var result = await _projectService.GetProject(ct, projectId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectPayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description,
            project.Tags);
    }

    public async Task<ProjectFilterPayload> ProjectsFilter(CancellationToken ct, ProjectFilterInput input)
    {
        var projects = await _projectService.FilterProjects(
            ct,
            input.Search,
            input.Tags ?? Array.Empty<string>(),
            input.Skip,
            input.Take
        );


        return new ProjectFilterPayload(projects.Select(x => new ProjectPayload(
            x.Id,
            x.OwnerUserId,
            x.ProjectName,
            x.Description,
            x.Tags
        )).ToArray());
    }

    public async Task<ProjectTeamParticipantPayload[]> ProjectTeamParticipantGet(CancellationToken ct,
        ProjectTeamParticipantInput input)
    {
        var result = await _projectTeamParticipantService.GetProjectTeamParticipantByProjectId(ct, input.ProjectId);
        result.ThrowQueryExceptionIfHasErrors();

        var teams = result.Value;

        return teams.Select(x => new ProjectTeamParticipantPayload(
            x.Id,
            x.TeamId,
            x.TeamName,
            x.ProjectId
        )).ToArray();
    }
}