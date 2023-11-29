using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events.ProjectTask;
using Garnet.Teams.Application.Team;

namespace Garnet.Teams.Infrastructure.EventHandlers.Project;

public class ProjectTaskClosedEventConsumer : IMessageBusConsumer<ProjectTaskClosedEvent>
{
    private readonly ITeamRepository _teamRepository;

    public ProjectTaskClosedEventConsumer(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task Consume(ProjectTaskClosedEvent message)
    {
        if (message.RatingCalculation is not null)
        {
            foreach (var team in message.RatingCalculation.TeamsTotalScore)
            {
                await _teamRepository.EditTeamTotalScore(CancellationToken.None, team.Key, team.Value);
            }
        }
    }
}