using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.Api.ProjectFilter;
using Garnet.Projects.Infrastructure.Api.ProjectGet;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestGet;
using Garnet.Projects.Infrastructure.Api.ProjectTeamParticipant;
using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api;

[ExtendObjectType("Query")]
public class ProjectsQuery
{
    private readonly ProjectService _projectService;
    private readonly ProjectTeamParticipantService _projectTeamParticipantService;
    private readonly ProjectTeamJoinRequestService _projectTeamJoinRequestService;

    public ProjectsQuery(ProjectService projectService, ProjectTeamParticipantService projectTeamParticipantService,
        ProjectTeamJoinRequestService projectTeamJoinRequestService)
    {
        _projectService = projectService;
        _projectTeamParticipantService = projectTeamParticipantService;
        _projectTeamJoinRequestService = projectTeamJoinRequestService;
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

    public async Task<ProjectTeamParticipantPayload> ProjectTeamParticipantsFilter(CancellationToken ct,
        ProjectTeamParticipantInput input)
    {
        var teams = await _projectTeamParticipantService.GetProjectTeamParticipantByProjectId(ct, input.ProjectId);

        return new ProjectTeamParticipantPayload(teams.Select(x => new ProjectTeamParticipantEntity(
            x.Id,
            x.TeamId,
            x.TeamName,
            x.ProjectId
        )).ToArray());
    }

    public async Task<ProjectTeamJoinRequestGetPayload> GetProjectTeamJoinRequestsByProjectId(CancellationToken ct,
        ClaimsPrincipal claims, ProjectTeamJoinRequestGetInput input)
    {
        var result =
            await _projectTeamJoinRequestService.GetProjectTeamJoinRequestsByProjectId(ct,
                new CurrentUserProvider(claims), input.ProjectId);
        result.ThrowQueryExceptionIfHasErrors();

        var teamJoinRequests = result.Value;
        return new ProjectTeamJoinRequestGetPayload(teamJoinRequests.Select(x => new ProjectTeamJoinRequestPayload(
            x.Id,
            x.TeamId,
            x.TeamName,
            x.ProjectId
        )).ToArray());
    }
}