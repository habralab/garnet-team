using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.Args;
using Garnet.Projects.Application.Project.Commands;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Projects.Application.ProjectUser.Commands;
using Garnet.Projects.Infrastructure.Api.ProjectCreate;
using Garnet.Projects.Infrastructure.Api.ProjectDelete;
using Garnet.Projects.Infrastructure.Api.ProjectEdit;
using Garnet.Projects.Infrastructure.Api.ProjectEditOwner;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestDecide;
using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api;

[ExtendObjectType("Mutation")]
public class ProjectsMutation
{
    private readonly ProjectCreateCommand _projectCreateCommand;
    private readonly ProjectDeleteCommand _projectDeleteCommand;
    private readonly ProjectEditDescriptionCommand _projectEditDescriptionCommand;
    private readonly ProjectEditOwnerCommand _projectEditOwnerCommand;
    private readonly ProjectTeamCreateCommand _projectTeamCreateCommand;
    private readonly ProjectUserCreateCommand _projectUserCreateCommand;
    private readonly ProjectTeamParticipantCreateCommand _projectTeamParticipantCreateCommand;
    private readonly ProjectTeamParticipantUpdateCommand _projectTeamParticipantUpdateCommand;
    private readonly ProjectTeamJoinRequestCreateCommand _projectTeamJoinRequestCreateCommand;
    private readonly ProjectTeamJoinRequestDecideCommand _projectTeamJoinRequestDecideCommand;
    private readonly ProjectTeamJoinRequestUpdateCommand _projectTeamJoinRequestUpdateCommand;


    public ProjectsMutation(
        ProjectCreateCommand projectCreateCommand, ProjectDeleteCommand projectDeleteCommand,
        ProjectEditDescriptionCommand projectEditDescriptionCommand, ProjectEditOwnerCommand projectEditOwnerCommand,
        ProjectTeamCreateCommand projectTeamCreateCommand, ProjectUserCreateCommand projectUserCreateCommand,
        ProjectTeamParticipantCreateCommand projectTeamParticipantCreateCommand,
        ProjectTeamParticipantUpdateCommand projectTeamParticipantUpdateCommand,
        ProjectTeamJoinRequestCreateCommand projectTeamJoinRequestCreateCommand,
        ProjectTeamJoinRequestDecideCommand projectTeamJoinRequestDecideCommand,
        ProjectTeamJoinRequestUpdateCommand projectTeamJoinRequestUpdateCommand
        )
    {
        _projectCreateCommand = projectCreateCommand;
        _projectDeleteCommand = projectDeleteCommand;
        _projectEditDescriptionCommand = projectEditDescriptionCommand;
        _projectEditOwnerCommand = projectEditOwnerCommand;
        _projectTeamCreateCommand = projectTeamCreateCommand;
        _projectUserCreateCommand = projectUserCreateCommand;
        _projectTeamParticipantCreateCommand = projectTeamParticipantCreateCommand;
        _projectTeamParticipantUpdateCommand = projectTeamParticipantUpdateCommand;
        _projectTeamJoinRequestCreateCommand = projectTeamJoinRequestCreateCommand;
        _projectTeamJoinRequestDecideCommand = projectTeamJoinRequestDecideCommand;
        _projectTeamJoinRequestUpdateCommand = projectTeamJoinRequestUpdateCommand;
    }

    public async Task<ProjectCreatePayload> ProjectCreate(CancellationToken ct, ClaimsPrincipal claims,
        ProjectCreateInput input)
    {
        var currentUserProvider = new CurrentUserProvider(claims);
        var args = new ProjectCreateArgs(input.ProjectName, currentUserProvider.UserId, input.Description, input.Tags);

        var result = await _projectCreateCommand.Execute(ct, args);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectCreatePayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description,
            project.Tags);
    }

    public async Task<ProjectEditDescriptionPayload> ProjectEditDescription(CancellationToken ct,
        ClaimsPrincipal claims, ProjectEditDescriptionInput input)
    {
        var result = await _projectEditDescriptionCommand.Execute(ct, new CurrentUserProvider(claims), input.ProjectId,
            input.Description);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditDescriptionPayload(project.Id, project.OwnerUserId, project.ProjectName,
            project.Description);
    }

    public async Task<ProjectDeletePayload?> ProjectDelete(CancellationToken ct, ClaimsPrincipal claims,
        string projectId)
    {
        var result = await _projectDeleteCommand.Execute(ct, new CurrentUserProvider(claims), projectId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectDeletePayload(project.Id, project.OwnerUserId, project.ProjectName,
            project.Description);
    }

    public async Task<ProjectEditOwnerPayload> ProjectEditOwner(CancellationToken ct, ClaimsPrincipal claims,
        ProjectEditOwnerInput input)
    {
        var result = await _projectEditOwnerCommand.Execute(ct, new CurrentUserProvider(claims), input.ProjectId,
            input.NewOwnerUserId);
        result.ThrowQueryExceptionIfHasErrors();

        var project = result.Value;
        return new ProjectEditOwnerPayload(project.Id, project.OwnerUserId, project.ProjectName, project.Description);
    }

    public async Task<ProjectTeamJoinRequestPayload> ProjectTeamJoinRequestDecide(CancellationToken ct,
        ClaimsPrincipal claims, ProjectTeamJoinRequestDecideInput input)
    {
        var result = await _projectTeamJoinRequestDecideCommand.Execute(ct, new CurrentUserProvider(claims),
            input.ProjectTeamJoinRequestId, input.IsApproved);
        result.ThrowQueryExceptionIfHasErrors();

        var teamJoinRequest = result.Value;
        return new ProjectTeamJoinRequestPayload(teamJoinRequest.Id, teamJoinRequest.TeamId, teamJoinRequest.TeamName,
            teamJoinRequest.ProjectId);
    }
}