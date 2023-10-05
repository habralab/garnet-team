using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers
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
            await _userService.UpdateUsername(CancellationToken.None, message.UserId, message.UserName);
            await _participantService.UpdateTeamParticipantUsername(CancellationToken.None, message.UserId, message.UserName);
        }
    }
}