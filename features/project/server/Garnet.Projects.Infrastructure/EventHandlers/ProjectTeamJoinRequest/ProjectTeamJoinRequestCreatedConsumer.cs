using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeam.Queries;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;
using Garnet.Teams.Events.TeamJoinProjectRequest;

namespace Garnet.Projects.Infrastructure.EventHandlers.ProjectTeamJoinRequest;

public class ProjectTeamJoinRequestCreatedConsumer : IMessageBusConsumer<TeamJoinProjectRequestCreatedEvent>
{
    private readonly ProjectTeamGetQuery _projectTeamGetQuery;
    private readonly ProjectTeamJoinRequestCreateCommand _projectTeamJoinRequestCreateCommand;

    public ProjectTeamJoinRequestCreatedConsumer(
        ProjectTeamJoinRequestCreateCommand projectTeamJoinRequestCreateCommand,
        ProjectTeamGetQuery projectTeamGetQuery)
    {
        _projectTeamJoinRequestCreateCommand = projectTeamJoinRequestCreateCommand;
        _projectTeamGetQuery = projectTeamGetQuery;
    }

    public async Task Consume(TeamJoinProjectRequestCreatedEvent message)
    {
        var team = await _projectTeamGetQuery.Query(CancellationToken.None, message.TeamId);
        await _projectTeamJoinRequestCreateCommand.Execute(CancellationToken.None, message.TeamId, team.TeamName,
            message.ProjectId);
    }
}