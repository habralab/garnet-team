using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamUser.Args;
using Garnet.Teams.Application.TeamUser.Commands;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers.User
{
    public class UserUpdatedEventConsumer : IMessageBusConsumer<UserUpdatedEvent>
    {
        private readonly TeamUserUpdateCommand _teamUserUpdateCommand;


        public UserUpdatedEventConsumer(TeamUserUpdateCommand teamUserUpdateCommand)
        {
            _teamUserUpdateCommand = teamUserUpdateCommand;
        }

        public async Task Consume(UserUpdatedEvent message)
        {
            var userUpdate = new TeamUserUpdateArgs(message.UserName, message.Tags, message.AvatarUrl);
            await _teamUserUpdateCommand.Execute(CancellationToken.None, message.UserId, userUpdate);
        }
    }
}