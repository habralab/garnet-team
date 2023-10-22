using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeam.Args;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Projects.Application.ProjectTeamParticipant.Args;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Teams.Events.TeamJoinInvitation;

namespace Garnet.Projects.Infrastructure.EventHandlers.Team;

public class TeamJoinInvitationDecidedEventConsumer: IMessageBusConsumer<TeamJoinInvitationDecidedEvent>
{
    private readonly ProjectTeamAddParticipantCommand _projectTeamAddParticipantCommand;
    private readonly ProjectTeamParticipantAddParticipantCommand _projectTeamParticipantAddParticipantCommand;

    public TeamJoinInvitationDecidedEventConsumer(
        ProjectTeamAddParticipantCommand projectTeamAddParticipantCommand,
        ProjectTeamParticipantAddParticipantCommand projectTeamParticipantAddParticipantCommand
    )
    {
        _projectTeamAddParticipantCommand = projectTeamAddParticipantCommand;
        _projectTeamParticipantAddParticipantCommand = projectTeamParticipantAddParticipantCommand;
    }

    public async Task Consume(TeamJoinInvitationDecidedEvent message)
    {
        if (message.IsApproved)
        {
            var projectTeamAddParticipantArgs = new ProjectTeamAddParticipantArgs(message.TeamId, message.UserId);
            await _projectTeamAddParticipantCommand.Execute(CancellationToken.None, projectTeamAddParticipantArgs);

            var projectTeamParticipantAddParticipantArgs = new ProjectTeamParticipantAddParticipantArgs(message.TeamId, message.UserId);;
            await _projectTeamParticipantAddParticipantCommand.Execute(CancellationToken.None,
                projectTeamParticipantAddParticipantArgs);
        }
    }
}