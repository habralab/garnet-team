using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeam.Args;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Projects.Application.ProjectTeamParticipant.Args;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Teams.Events.TeamUserJoinRequest;

namespace Garnet.Projects.Infrastructure.EventHandlers.Team;

public class TeamUserJoinRequestDecidedEventConsumer : IMessageBusConsumer<TeamUserJoinRequestDecidedEvent>
{
    private readonly ProjectTeamAddUserParticipantCommand _projectTeamAddUserParticipantCommand;
    private readonly ProjectTeamParticipantAddUserParticipantCommand _projectTeamParticipantAddUserParticipantCommand;

    public TeamUserJoinRequestDecidedEventConsumer(
        ProjectTeamAddUserParticipantCommand projectTeamAddUserParticipantCommand,
        ProjectTeamParticipantAddUserParticipantCommand projectTeamParticipantAddUserParticipantCommand
        )
    {
        _projectTeamAddUserParticipantCommand = projectTeamAddUserParticipantCommand;
        _projectTeamParticipantAddUserParticipantCommand = projectTeamParticipantAddUserParticipantCommand;
    }

    public async Task Consume(TeamUserJoinRequestDecidedEvent message)
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