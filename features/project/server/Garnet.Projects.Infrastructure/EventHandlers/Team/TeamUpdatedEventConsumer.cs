using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeam.Args;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Args;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;
using Garnet.Projects.Application.ProjectTeamParticipant.Args;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Teams.Events.Team;

namespace Garnet.Projects.Infrastructure.EventHandlers.Team;

public class TeamUpdatedEventConsumer : IMessageBusConsumer<TeamUpdatedEvent>
{
    private readonly ProjectTeamUpdateCommand _projectTeamUpdateCommand;
    private readonly ProjectTeamParticipantUpdateCommand _projectTeamParticipantUpdateCommand;
    private readonly ProjectTeamJoinRequestUpdateCommand _projectTeamJoinRequestUpdateCommand;

    public TeamUpdatedEventConsumer(ProjectTeamUpdateCommand projectTeamUpdateCommand,
        ProjectTeamParticipantUpdateCommand projectTeamParticipantUpdateCommand,
        ProjectTeamJoinRequestUpdateCommand projectTeamJoinRequestUpdateCommand)
    {
        _projectTeamUpdateCommand = projectTeamUpdateCommand;
        _projectTeamParticipantUpdateCommand = projectTeamParticipantUpdateCommand;
        _projectTeamJoinRequestUpdateCommand = projectTeamJoinRequestUpdateCommand;
    }

    public async Task Consume(TeamUpdatedEvent message)
    {
        var projectTeamUpdateArgs = new ProjectTeamUpdateArgs(message.Id, message.Name, message.OwnerUserId);
        await _projectTeamUpdateCommand.Execute(CancellationToken.None, projectTeamUpdateArgs);

        var projectTeamParticipantUpdateArgs = new ProjectTeamParticipantUpdateArgs(message.Id, message.Name);
        await _projectTeamParticipantUpdateCommand.Execute(CancellationToken.None, projectTeamParticipantUpdateArgs);

        var projectTeamJoinRequestUpdateArgs = new ProjectTeamJoinRequestUpdateArgs(message.Id, message.Name);
        await _projectTeamJoinRequestUpdateCommand.Execute(CancellationToken.None, projectTeamJoinRequestUpdateArgs);
    }
}