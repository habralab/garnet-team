using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.Project.Args;
using Garnet.Projects.Application.Project.Queries;
using Garnet.Projects.Application.ProjectTask.Queries;
using Garnet.Projects.Application.ProjectTeam.Queries;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Queries;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Application.ProjectTeamParticipant.Queries;
using Garnet.Projects.Application.ProjectUser.Queries;
using Garnet.Projects.Infrastructure.Api.ProjectCreate;
using Garnet.Projects.Infrastructure.Api.ProjectFilter;
using Garnet.Projects.Infrastructure.Api.ProjectFilterByTeamParticipantId;
using Garnet.Projects.Infrastructure.Api.ProjectFilterByUserParticipantId;
using Garnet.Projects.Infrastructure.Api.ProjectGet;
using Garnet.Projects.Infrastructure.Api.ProjectTaskGet;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestGet;
using Garnet.Projects.Infrastructure.Api.ProjectTeamParticipant;
using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api;

[ExtendObjectType("Query")]
public class ProjectsQuery
{
    private readonly ProjectGetQuery _projectGetQuery;
    private readonly ProjectsFilterQuery _projectsFilterQuery;
    private readonly ProjectTeamParticipantFilterQuery _projectTeamParticipantFilterQuery;
    private readonly ProjectTeamJoinRequestFilterQuery _projectTeamJoinRequestFilterQuery;
    private readonly ProjectFilterByUserParticipantIdQuery _projectFilterByUserParticipantIdQuery;
    private readonly ProjectFilterByTeamParticipantIdQuery _projectFilterByTeamParticipantIdQuery;
    private readonly ProjectTaskGetQuery _projectTaskGetQuery;
    private readonly ProjectTeamGetQuery _projectTeamGetQuery;

    public ProjectsQuery(
        ProjectGetQuery projectGetQuery,
        ProjectsFilterQuery projectsFilterQuery,
        ProjectTeamParticipantFilterQuery projectTeamParticipantFilterQuery,
        ProjectTeamJoinRequestFilterQuery projectTeamJoinRequestFilterQuery,
        ProjectTaskGetQuery projectTaskGetQuery,
        ProjectFilterByUserParticipantIdQuery projectFilterByUserParticipantIdQuery,
        ProjectFilterByTeamParticipantIdQuery projectFilterByTeamParticipantIdQuery,
        ProjectTeamGetQuery projectTeamGetQuery)
    {
        _projectGetQuery = projectGetQuery;
        _projectsFilterQuery = projectsFilterQuery;
        _projectTeamParticipantFilterQuery = projectTeamParticipantFilterQuery;
        _projectTeamJoinRequestFilterQuery = projectTeamJoinRequestFilterQuery;
        _projectTaskGetQuery = projectTaskGetQuery;
        _projectFilterByUserParticipantIdQuery = projectFilterByUserParticipantIdQuery;
        _projectFilterByTeamParticipantIdQuery = projectFilterByTeamParticipantIdQuery;
        _projectTeamGetQuery = projectTeamGetQuery;
    }

    public async Task<ProjectPayload> ProjectGet(CancellationToken ct, string projectId)
    {
        var result = await _projectGetQuery.Query(ct, projectId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectPayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description,
            project.AvatarUrl, project.Tags);
    }

    public async Task<ProjectFilterPayload> ProjectsFilter(CancellationToken ct, ProjectFilterInput input)
    {
        var args = new ProjectFilterArgs(
            input.Search,
            input.Tags ?? Array.Empty<string>(),
            input.Skip,
            input.Take);

        var result = await _projectsFilterQuery.Query(ct, args);
        result.ThrowQueryExceptionIfHasErrors();

        var projects = result.Value;
        return new ProjectFilterPayload(projects.Select(x => new ProjectPayload(
            x.Id,
            x.OwnerUserId,
            x.ProjectName,
            x.Description,
            x.AvatarUrl,
            x.Tags
        )).ToArray());
    }

    public async Task<ProjectTeamParticipantPayload> ProjectTeamParticipantsFilter(CancellationToken ct,
        ProjectTeamParticipantInput input)
    {
        var teams = await _projectTeamParticipantFilterQuery.Query(ct, input.ProjectId);

        return new ProjectTeamParticipantPayload(teams.Select(x => new ProjectTeamParticipantEntity(
            x.Id,
            x.TeamId,
            x.TeamName,
            x.ProjectId,
            x.TeamAvatarUrl,
            x.UserParticipants,
            x.Projects
        )).ToArray());
    }

    public async Task<ProjectFilterByUserParticipantIdPayload> ProjectFilterByUserParticipantId(CancellationToken ct,
        string userId)
    {
        var projectList = await _projectFilterByUserParticipantIdQuery.Query(ct, userId);
        var projects = projectList.Select(x => new ProjectCreatePayload(
            x.Id,
            x.OwnerUserId,
            x.ProjectName,
            x.Description,
            x.AvatarUrl,
            x.Tags));
        return new ProjectFilterByUserParticipantIdPayload(projects.ToArray());
    }

    public async Task<ProjectFilterByTeamParticipantIdPayload> ProjectFilterByTeamParticipantId(CancellationToken ct,
        string teamId)
    {
        var projectList = await _projectFilterByTeamParticipantIdQuery.Query(ct, teamId);
        var projects = projectList.Select(x => new ProjectCreatePayload(
            x.Id,
            x.OwnerUserId,
            x.ProjectName,
            x.Description,
            x.AvatarUrl,
            x.Tags));
        return new ProjectFilterByTeamParticipantIdPayload(projects.ToArray());
    }

    public async Task<ProjectTeamJoinRequestGetPayload> GetProjectTeamJoinRequestsByProjectId(
        CancellationToken ct,
        ProjectTeamJoinRequestGetInput input)
    {
        var result =
            await _projectTeamJoinRequestFilterQuery.Query(ct, input.ProjectId);
        result.ThrowQueryExceptionIfHasErrors();

        var teamJoinRequests = result.Value;

        List<ProjectTeamJoinRequestPayload> payload = new();

        foreach (var teamJoinRequest in teamJoinRequests)
        {
            var team = await _projectTeamGetQuery.Query(ct, teamJoinRequest.TeamId);
            var teamParticipant = await _projectTeamParticipantFilterQuery.Query(ct, teamJoinRequest.TeamId);

            payload.Add(new ProjectTeamJoinRequestPayload(
                teamJoinRequest.Id,
                teamJoinRequest.TeamId,
                teamJoinRequest.TeamName,
                team.TeamDescription,
                team.TeamAvatarUrl,
                teamJoinRequest.ProjectId,
                teamParticipant.Length,
                team.UserParticipantIds.Length
            ));
        }

        return new ProjectTeamJoinRequestGetPayload(payload.ToArray());
    }

    public async Task<ProjectTaskGetPayload> ProjectTaskGetById(CancellationToken ct, string taskId)
    {
        var result = await _projectTaskGetQuery.Query(ct, taskId);
        result.ThrowQueryExceptionIfHasErrors();

        var task = result.Value;
        return new ProjectTaskGetPayload(
            task.Id, task.TaskNumber, task.ProjectId, task.ResponsibleUserId, task.Name, task.Description,
            task.Status, task.TeamExecutorIds, task.UserExecutorIds, task.Tags, task.Labels);
    }
}