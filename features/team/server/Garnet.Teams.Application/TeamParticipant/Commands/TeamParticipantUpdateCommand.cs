using Garnet.Teams.Application.TeamParticipant.Args;

namespace Garnet.Teams.Application.TeamParticipant.Commands
{
    public class TeamParticipantUpdateCommand
    {
        private readonly ITeamParticipantRepository _teamParticipantsRepository;

        public TeamParticipantUpdateCommand(ITeamParticipantRepository teamParticipantsRepository)
        {
            _teamParticipantsRepository = teamParticipantsRepository;
        }

        public async Task Execute(CancellationToken ct, string userId, TeamParticipantUpdateArgs update)
        {
            await _teamParticipantsRepository.UpdateTeamParticipant(ct, userId, update);
        }
    }
}