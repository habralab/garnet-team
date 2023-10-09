using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Args;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers.User
{
    public class UserUpdatedEventConsumer : IMessageBusConsumer<UserUpdatedEvent>
    {
        private readonly TeamUserService _userService;
        private readonly TeamParticipantService _participantService;

        public UserUpdatedEventConsumer(
            TeamUserService userService,
            TeamParticipantService participantService)
        {
            _userService = userService;
            _participantService = participantService;
        }

        public async Task Consume(UserUpdatedEvent message)
        {
            var userUpdate = new TeamUserUpdateArgs(message.UserName);
            var participantUpdate = new TeamParticipantUpdateArgs(message.UserName);

            await _userService.UpdateUser(CancellationToken.None, message.UserId, userUpdate);
            await _participantService.UpdateTeamParticipant(CancellationToken.None, message.UserId, participantUpdate);
        }
    }
}