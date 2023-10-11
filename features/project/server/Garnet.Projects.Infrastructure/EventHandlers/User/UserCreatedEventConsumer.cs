using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectUser.Commands;
using Garnet.Users.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers.User
{
    public class UserCreatedEventConsumer : IMessageBusConsumer<UserCreatedEvent>
    {
        private readonly ProjectUserCreateCommand _projectUserCreateCommand;
        public UserCreatedEventConsumer(ProjectUserCreateCommand projectUserCreateCommand)
        {
            _projectUserCreateCommand = projectUserCreateCommand;
        }

        public async Task Consume(UserCreatedEvent message)
        {
            await _projectUserCreateCommand.Execute(CancellationToken.None, message.UserId);
        }
    }
}