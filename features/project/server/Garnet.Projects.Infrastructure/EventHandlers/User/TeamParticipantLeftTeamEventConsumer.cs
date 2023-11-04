using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Teams.Events.TeamParticipant;

namespace Garnet.Projects.Infrastructure.EventHandlers.User;

public class TeamParticipantLeftTeamEventConsumer : IMessageBusConsumer<TeamParticipantLeftTeamEvent>
{
    private readonly ProjectTeamDeleteUserParticipantCommand _projectTeamDeleteUserParticipantCommand;
    private readonly ProjectTeamParticipantDeleteUserParticipantCommand _projectTeamParticipantDeleteUserParticipantCommand;

    public TeamParticipantLeftTeamEventConsumer(ProjectTeamDeleteUserParticipantCommand projectTeamDeleteUserParticipantCommand, ProjectTeamParticipantDeleteUserParticipantCommand projectTeamParticipantDeleteUserParticipantCommand)
    {
        _projectTeamDeleteUserParticipantCommand = projectTeamDeleteUserParticipantCommand;
        _projectTeamParticipantDeleteUserParticipantCommand = projectTeamParticipantDeleteUserParticipantCommand;
    }


    public async Task Consume(TeamParticipantLeftTeamEvent message)
    {
        await _projectTeamDeleteUserParticipantCommand.Execute(CancellationToken.None, message.TeamId, message.UserId);
        await _projectTeamParticipantDeleteUserParticipantCommand.Execute(CancellationToken.None, message.TeamId, message.UserId);
    }
}
