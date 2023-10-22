using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeam.Args;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Projects.Application.ProjectTeamParticipant.Args;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Teams.Events.TeamUserJoinRequest;

namespace Garnet.Projects.Infrastructure.EventHandlers.Team;

public class TeamUserJoinRequestDecidedEventConsumer : IMessageBusConsumer<TeamUserJoinRequestDecidedEvent>
{
    private readonly ProjectTeamAddParticipantCommand _projectTeamAddParticipantCommand;
    private readonly ProjectTeamParticipantAddParticipantCommand _projectTeamParticipantAddParticipantCommand;

    public TeamUserJoinRequestDecidedEventConsumer(
        ProjectTeamAddParticipantCommand projectTeamAddParticipantCommand,
        ProjectTeamParticipantAddParticipantCommand projectTeamParticipantAddParticipantCommand
        )
    {
        _projectTeamAddParticipantCommand = projectTeamAddParticipantCommand;
        _projectTeamParticipantAddParticipantCommand = projectTeamParticipantAddParticipantCommand;
    }

    public async Task Consume(TeamUserJoinRequestDecidedEvent message)
    {
        if (message.IsApproved)
        {
            var projectTeamAddParticipantArgs = new ProjectTeamAddParticipantArgs(message.UserId, message.TeamId);
            await _projectTeamAddParticipantCommand.Execute(CancellationToken.None, projectTeamAddParticipantArgs);

            var projectTeamParticipantAddParticipantArgs = new ProjectTeamParticipantAddParticipantArgs(message.UserId, message.TeamId);;
            await _projectTeamParticipantAddParticipantCommand.Execute(CancellationToken.None,
                projectTeamParticipantAddParticipantArgs);
        }
    }
}