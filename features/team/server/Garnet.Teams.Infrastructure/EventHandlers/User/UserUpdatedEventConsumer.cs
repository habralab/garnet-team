using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Args;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers.User
{
    public class UserUpdatedEventConsumer : IMessageBusConsumer<UserUpdatedEvent>
    {
        private readonly ITeamUserRepository _teamUserRepository;
        private readonly ITeamParticipantRepository _teamParticipantsRepository;


        public UserUpdatedEventConsumer(
            ITeamUserRepository teamUserRepository,
            ITeamParticipantRepository teamParticipantsRepository)
        {
            _teamUserRepository = teamUserRepository;
            _teamParticipantsRepository = teamParticipantsRepository;
        }

        public async Task Consume(UserUpdatedEvent message)
        {
            var userUpdate = new TeamUserUpdateArgs(message.UserName);
            var participantUpdate = new TeamParticipantUpdateArgs(message.UserName);

            await _teamUserRepository.UpdateUser(CancellationToken.None, message.UserId, userUpdate);
            await _teamParticipantsRepository.UpdateTeamParticipant(CancellationToken.None, message.UserId, participantUpdate);
        }
    }
}