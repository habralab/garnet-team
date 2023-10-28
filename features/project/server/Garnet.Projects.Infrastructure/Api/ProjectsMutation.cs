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
using Garnet.Projects.Infrastructure.Api.ProjectTask;
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
    private readonly ProjectTaskCreateCommand _projectTaskCreateCommand;


    public ProjectsMutation(
        ProjectCreateCommand projectCreateCommand,
        ProjectDeleteCommand projectDeleteCommand,
        ProjectEditDescriptionCommand projectEditDescriptionCommand,
        ProjectEditOwnerCommand projectEditOwnerCommand,
        ProjectTeamJoinRequestDecideCommand projectTeamJoinRequestDecideCommand,
        ProjectUploadAvatarCommand projectUploadAvatarCommand,
        ProjectEditNameCommand projectEditNameCommand, ProjectTaskCreateCommand projectTaskCreateCommand)
    {
        _projectCreateCommand = projectCreateCommand;
        _projectDeleteCommand = projectDeleteCommand;
        _projectEditDescriptionCommand = projectEditDescriptionCommand;
        _projectEditOwnerCommand = projectEditOwnerCommand;
        _projectTeamJoinRequestDecideCommand = projectTeamJoinRequestDecideCommand;
        _projectUploadAvatarCommand = projectUploadAvatarCommand;
        _projectEditNameCommand = projectEditNameCommand;
        _projectTaskCreateCommand = projectTaskCreateCommand;
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
            project.Description);
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
            project.Description);
    }

    public async Task<ProjectEditOwnerPayload> ProjectEditOwner(CancellationToken ct,
        ProjectEditOwnerInput input)
    {
        var result = await _projectEditOwnerCommand.Execute(ct, input.ProjectId,
            input.NewOwnerUserId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditOwnerPayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description);
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
            input.TeamExecutorId,
            input.UserExecutorIds,
            input.Tags,
            input.Labels);

        var result = await _projectTaskCreateCommand.Execute(ct, args);
        result.ThrowQueryExceptionIfHasErrors();

        var task = result.Value;
        return new ProjectTaskCreatePayload(
            task.Id, task.ProjectId, task.ResponsibleUserId, task.Name, task.Description,
            task.Status, task.TeamExecutorId, task.UserExecutorIds, task.Tags, task.Labels);
    }
}