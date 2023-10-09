using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamUser.Args;
using Garnet.Teams.Application.TeamUser.Commands;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers.User
{
    public class UserUpdatedEventConsumer : IMessageBusConsumer<UserUpdatedEvent>
    {
        private readonly TeamUserUpdateCommand _teamUserUpdateCommand;
        private readonly TeamParticipantService _participantService;

        public UserUpdatedEventConsumer(
            TeamUserUpdateCommand teamUserUpdateCommand,
            TeamParticipantService participantService)
        {
            _teamUserUpdateCommand = teamUserUpdateCommand;
            _participantService = participantService;
        }

        public async Task Consume(UserUpdatedEvent message)
        {
            var userUpdate = new TeamUserUpdateArgs(message.UserName);
            var participantUpdate = new TeamParticipantUpdateArgs(message.UserName);

            await _teamUserUpdateCommand.Execute(CancellationToken.None, message.UserId, userUpdate);
            await _participantService.UpdateTeamParticipant(CancellationToken.None, message.UserId, participantUpdate);
        }
    }
}