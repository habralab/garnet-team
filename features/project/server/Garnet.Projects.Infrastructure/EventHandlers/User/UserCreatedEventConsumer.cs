using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application;
using Garnet.Users.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers
{
    public class UserCreatedEventConsumer : IMessageBusConsumer<UserCreatedEvent>
    {
        private readonly ProjectUserService _projectUserService;
        public UserCreatedEventConsumer(ProjectUserService projectUserService)
        {
            _projectUserService = projectUserService;
        }

        public async Task Consume(UserCreatedEvent message)
        {
            await _projectUserService.AddUser(CancellationToken.None, message.UserId);
        }
    }
}