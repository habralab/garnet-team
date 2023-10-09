using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamParticipant.Commands;
using Garnet.Teams.Application.TeamUser.Args;
using Garnet.Teams.Application.TeamUser.Commands;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers.User
{
    public class UserUpdatedEventConsumer : IMessageBusConsumer<UserUpdatedEvent>
    {
        private readonly TeamUserUpdateCommand _teamUserUpdateCommand;
        private readonly TeamParticipantUpdateCommand _teamParticipantUpdateCommand;

        public UserUpdatedEventConsumer(
            TeamUserUpdateCommand teamUserUpdateCommand,
            TeamParticipantUpdateCommand teamParticipantUpdateCommand)
        {
            _teamUserUpdateCommand = teamUserUpdateCommand;
            _teamParticipantUpdateCommand = teamParticipantUpdateCommand;
        }

        public async Task Consume(UserUpdatedEvent message)
        {
            var userUpdate = new TeamUserUpdateArgs(message.UserName);
            var participantUpdate = new TeamParticipantUpdateArgs(message.UserName);

            await _teamUserUpdateCommand.Execute(CancellationToken.None, message.UserId, userUpdate);
            await _teamParticipantUpdateCommand.Execute(CancellationToken.None, message.UserId, participantUpdate);
        }
    }
}