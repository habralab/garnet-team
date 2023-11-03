using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectUser.Commands;
using Garnet.Users.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers.User;

public class UserUpdatedEventConsumer : IMessageBusConsumer<UserUpdatedEvent>
{
    private readonly ProjectUserUpdateCommand _projectUserUpdateCommand;

    public UserUpdatedEventConsumer(ProjectUserUpdateCommand projectUserUpdateCommand)
    {
        _projectUserUpdateCommand = projectUserUpdateCommand;
    }

    public async Task Consume(UserUpdatedEvent message)
    {
        await _projectUserUpdateCommand.Execute(CancellationToken.None, message.UserId, message.UserName,
            message.AvatarUrl);
    }
}