using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamUser.Args;

namespace Garnet.Teams.Application.TeamUser.Commands
{
    public class TeamUserUpdateCommand
    {
        private readonly ITeamUserRepository _teamUserRepository;
        private readonly ITeamParticipantRepository _teamParticipantsRepository;

        public TeamUserUpdateCommand(
            ITeamUserRepository teamUserRepository,
            ITeamParticipantRepository teamParticipantsRepository)
        {
            _teamUserRepository = teamUserRepository;
            _teamParticipantsRepository = teamParticipantsRepository;
        }

        public async Task Execute(CancellationToken ct, string userId, TeamUserUpdateArgs args)
        {
            var user = await _teamUserRepository.GetUser(ct, userId);
            await _teamUserRepository.UpdateUser(CancellationToken.None, userId, args);

            var participantUpdate = new TeamParticipantUpdateArgs(args.Username, args.AvatarUrl);
            await _teamParticipantsRepository.UpdateTeamParticipant(CancellationToken.None, userId, participantUpdate);
        }
    }
}