using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events.ProjectTask;
using Garnet.Users.Application;

namespace Garnet.Users.Infrastructure.EventHandlers;

public class ProjectTaskClosedEventConsumer : IMessageBusConsumer<ProjectTaskClosedEvent>
{
    private readonly IUsersRepository _usersRepository;

    public ProjectTaskClosedEventConsumer(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task Consume(ProjectTaskClosedEvent message)
    {
        if (message.RatingCalculation is not null)
        {
            foreach (var userId in message.RatingCalculation.UserExecutorIds)
            {
                await _usersRepository.EditUserTotalScore(userId, message.RatingCalculation.UserTotalScore);
                await _usersRepository.EditUserSkillScore(userId, message.RatingCalculation.SkillScorePerUser);
            }

            await _usersRepository.EditUserTotalScore(
                message.RatingCalculation.ProjectOwnerId,
                message.RatingCalculation.ProjectOwnerTotalScore);
        }
    }
}