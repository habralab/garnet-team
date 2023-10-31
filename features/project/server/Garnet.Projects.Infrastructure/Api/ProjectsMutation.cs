using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.Project.Args;
using Garnet.Projects.Application.Project.Commands;
using Garnet.Projects.Application.ProjectTask.Args;
using Garnet.Projects.Application.ProjectTask.Commands;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;
using Garnet.Projects.Infrastructure.Api.ProjectCreate;
using Garnet.Projects.Infrastructure.Api.ProjectDelete;
using Garnet.Projects.Infrastructure.Api.ProjectEditDescription;
using Garnet.Projects.Infrastructure.Api.ProjectEditName;
using Garnet.Projects.Infrastructure.Api.ProjectEditOwner;
using Garnet.Projects.Infrastructure.Api.ProjectEditTags;
using Garnet.Projects.Infrastructure.Api.ProjectTaskClose;
using Garnet.Projects.Infrastructure.Api.ProjectTaskCreate;
using Garnet.Projects.Infrastructure.Api.ProjectTaskDelete;
using Garnet.Projects.Infrastructure.Api.ProjectTaskEditName;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestDecide;
using Garnet.Projects.Infrastructure.Api.ProjectUploadAvatar;
using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api;

[ExtendObjectType("Mutation")]
public class ProjectsMutation
{
    private readonly ProjectCreateCommand _projectCreateCommand;
    private readonly ProjectDeleteCommand _projectDeleteCommand;
    private readonly ProjectEditDescriptionCommand _projectEditDescriptionCommand;
    private readonly ProjectEditOwnerCommand _projectEditOwnerCommand;
    private readonly ProjectTeamJoinRequestDecideCommand _projectTeamJoinRequestDecideCommand;
    private readonly ProjectUploadAvatarCommand _projectUploadAvatarCommand;
    private readonly ProjectEditNameCommand _projectEditNameCommand;
    private readonly ProjectEditTagsCommand _projectEditTagsCommand;
    private readonly ProjectTaskCreateCommand _projectTaskCreateCommand;
    private readonly ProjectTaskDeleteCommand _projectTaskDeleteCommand;
    private readonly ProjectTaskEditNameCommand _projectTaskEditNameCommand;
    private readonly ProjectTaskCloseCommand _projectTaskCloseCommand;


    public ProjectsMutation(
        ProjectCreateCommand projectCreateCommand,
        ProjectDeleteCommand projectDeleteCommand,
        ProjectEditDescriptionCommand projectEditDescriptionCommand,
        ProjectEditOwnerCommand projectEditOwnerCommand,
        ProjectTeamJoinRequestDecideCommand projectTeamJoinRequestDecideCommand,
        ProjectUploadAvatarCommand projectUploadAvatarCommand,
        ProjectEditNameCommand projectEditNameCommand,
        ProjectEditTagsCommand projectEditTagsCommand,
        ProjectTaskCreateCommand projectTaskCreateCommand,
        ProjectTaskDeleteCommand projectTaskDeleteCommand,
        ProjectTaskEditNameCommand projectTaskEditNameCommand,
        ProjectTaskCloseCommand projectTaskCloseCommand
    )
    {
        _projectCreateCommand = projectCreateCommand;
        _projectDeleteCommand = projectDeleteCommand;
        _projectEditDescriptionCommand = projectEditDescriptionCommand;
        _projectEditOwnerCommand = projectEditOwnerCommand;
        _projectTeamJoinRequestDecideCommand = projectTeamJoinRequestDecideCommand;
        _projectUploadAvatarCommand = projectUploadAvatarCommand;
        _projectEditNameCommand = projectEditNameCommand;
        _projectEditTagsCommand = projectEditTagsCommand;
        _projectTaskCreateCommand = projectTaskCreateCommand;
        _projectTaskDeleteCommand = projectTaskDeleteCommand;
        _projectTaskEditNameCommand = projectTaskEditNameCommand;
        _projectTaskEditNameCommand = projectTaskEditNameCommand;
        _projectTaskCloseCommand = projectTaskCloseCommand;
    }

    public async Task<ProjectCreatePayload> ProjectCreate(CancellationToken ct,
        ProjectCreateInput input)
    {
        var avatarFile = input.File is null
            ? null
            : new AvatarFileArgs(
                input.File.Name,
                input.File.ContentType,
                input.File.OpenReadStream());
        var args = new ProjectCreateArgs(input.ProjectName, input.Description,
            avatarFile, input.Tags);

        var result = await _projectCreateCommand.Execute(ct, args);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectCreatePayload(project.Id, project.OwnerUserId, project.ProjectName,
            project.Description,
            project.AvatarUrl,
            project.Tags);
    }

    public async Task<ProjectEditDescriptionPayload> ProjectEditDescription(CancellationToken ct,
        ProjectEditDescriptionInput input)
    {
        var result = await _projectEditDescriptionCommand.Execute(ct, input.ProjectId,
            input.Description);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditDescriptionPayload(project.Id, project.OwnerUserId, project.ProjectName,
            project.Description, project.AvatarUrl, project.Tags);
    }

    public async Task<ProjectUploadAvatarPayload> ProjectUploadAvatar(CancellationToken ct,
        ProjectUploadAvatarInput input)
    {
        var result = await _projectUploadAvatarCommand.Execute(ct, input.ProjectId,
            input.File.ContentType, input.File.OpenReadStream());
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectUploadAvatarPayload(project.Id, project.OwnerUserId, project.ProjectName,
            project.Description, project.AvatarUrl, project.Tags);
    }

    public async Task<ProjectDeletePayload?> ProjectDelete(CancellationToken ct,
        string projectId)
    {
        var result = await _projectDeleteCommand.Execute(ct, projectId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectDeletePayload(project.Id, project.OwnerUserId, project.ProjectName,
            project.Description, project.AvatarUrl, project.Tags);
    }

    public async Task<ProjectEditOwnerPayload> ProjectEditOwner(CancellationToken ct,
        ProjectEditOwnerInput input)
    {
        var result = await _projectEditOwnerCommand.Execute(ct, input.ProjectId,
            input.NewOwnerUserId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditOwnerPayload(project.Id, project.OwnerUserId, project.ProjectName,
            project.Description, project.AvatarUrl, project.Tags);
    }

    public async Task<ProjectEditNamePayload> ProjectEditName(CancellationToken ct,
        ProjectEditNameInput input)
    {
        var result = await _projectEditNameCommand.Execute(ct, input.Id,
            input.NewName);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditNamePayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description,
            project.AvatarUrl,
            project.Tags);
    }

    public async Task<ProjectEditTagsPayload> ProjectEditTags(CancellationToken ct,
        ProjectEditTagsInput input)
    {
        var result = await _projectEditTagsCommand.Execute(ct, input.ProjectId,
            input.Tags);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditTagsPayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description,
            project.AvatarUrl,
            project.Tags);
    }


    public async Task<ProjectTeamJoinRequestPayload> ProjectTeamJoinRequestDecide(CancellationToken ct,
        ProjectTeamJoinRequestDecideInput input)
    {
        var result = await _projectTeamJoinRequestDecideCommand.Execute(ct,
            input.ProjectTeamJoinRequestId, input.IsApproved);
        result.ThrowQueryExceptionIfHasErrors();

        var teamJoinRequest = result.Value;
        return new ProjectTeamJoinRequestPayload(teamJoinRequest.Id, teamJoinRequest.TeamId, teamJoinRequest.TeamName,
            teamJoinRequest.ProjectId);
    }

    public async Task<ProjectTaskCreatePayload> ProjectTaskCreate(CancellationToken ct,
        ProjectTaskCreateInput input)
    {
        var args = new ProjectTaskCreateArgs(
            input.ProjectId,
            input.Name,
            input.Description,
            input.TeamExecutorIds,
            input.UserExecutorIds,
            input.Tags,
            input.Labels);

        var result = await _projectTaskCreateCommand.Execute(ct, args);
        result.ThrowQueryExceptionIfHasErrors();

        var task = result.Value;
        return new ProjectTaskCreatePayload(
            task.Id, task.TaskNumber, task.ProjectId, task.ResponsibleUserId, task.Name, task.Description,
            task.Status, task.TeamExecutorIds, task.UserExecutorIds, task.Tags, task.Labels);
    }


    public async Task<ProjectTaskDeletePayload> ProjectTaskDelete(CancellationToken ct, string taskId)
    {
        var result = await _projectTaskDeleteCommand.Execute(ct, taskId);
        result.ThrowQueryExceptionIfHasErrors();

        var task = result.Value;
        return new ProjectTaskDeletePayload(
            task.Id, task.TaskNumber, task.ProjectId, task.ResponsibleUserId, task.Name, task.Description,
            task.Status, task.TeamExecutorIds, task.UserExecutorIds, task.Tags, task.Labels);
    }

    public async Task<ProjectTaskEditNamePayload> ProjectTaskEditName(CancellationToken ct,
        string taskId, string newTaskName)
    {
        var result = await _projectTaskEditNameCommand.Execute(ct, taskId, newTaskName);
        result.ThrowQueryExceptionIfHasErrors();

        var task = result.Value;
        return new ProjectTaskEditNamePayload(
            task.Id, task.TaskNumber, task.ProjectId, task.ResponsibleUserId, task.Name, task.Description,
            task.Status, task.TeamExecutorIds, task.UserExecutorIds, task.Tags, task.Labels);
    }

    public async Task<ProjectTaskClosePayload> ProjectTaskClose(CancellationToken ct, string taskId)
    {
        var result = await _projectTaskCloseCommand.Execute(ct, taskId);
        result.ThrowQueryExceptionIfHasErrors();

        var task = result.Value;
        return new ProjectTaskClosePayload(
            task.Id, task.TaskNumber, task.ProjectId, task.ResponsibleUserId, task.Name, task.Description,
            task.Status, task.TeamExecutorIds, task.UserExecutorIds, task.Tags, task.Labels);
    }
}