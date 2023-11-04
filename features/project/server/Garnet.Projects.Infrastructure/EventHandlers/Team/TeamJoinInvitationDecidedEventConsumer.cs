using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeam.Args;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Projects.Application.ProjectTeamParticipant.Args;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Teams.Events.TeamJoinInvitation;

namespace Garnet.Projects.Infrastructure.EventHandlers.Team;

public class TeamJoinInvitationDecidedEventConsumer: IMessageBusConsumer<TeamJoinInvitationDecidedEvent>
{
    private readonly ProjectTeamAddUserParticipantCommand _projectTeamAddUserParticipantCommand;
    private readonly ProjectTeamParticipantAddUserParticipantCommand _projectTeamParticipantAddUserParticipantCommand;

    public TeamJoinInvitationDecidedEventConsumer(
        ProjectTeamAddUserParticipantCommand projectTeamAddUserParticipantCommand,
        ProjectTeamParticipantAddUserParticipantCommand projectTeamParticipantAddUserParticipantCommand
    )
    {
        _projectTeamAddUserParticipantCommand = projectTeamAddUserParticipantCommand;
        _projectTeamParticipantAddUserParticipantCommand = projectTeamParticipantAddUserParticipantCommand;
    }

    public async Task Consume(TeamJoinInvitationDecidedEvent message)
    {
        if (message.IsApproved)
        {
            var projectTeamAddParticipantArgs = new ProjectTeamAddParticipantArgs(message.UserId, message.TeamId);
            await _projectTeamAddUserParticipantCommand.Execute(CancellationToken.None, projectTeamAddParticipantArgs);

            var projectTeamParticipantAddParticipantArgs = new ProjectTeamParticipantAddParticipantArgs(message.UserId, message.TeamId);;
            await _projectTeamParticipantAddUserParticipantCommand.Execute(CancellationToken.None,
                projectTeamParticipantAddParticipantArgs);
        }
    }
}