using Garnet.Teams.Application.TeamParticipant.Args;

namespace Garnet.Teams.Application.TeamParticipant.Queries
{
    public class TeamParticipantFilterQuery
    {
        private readonly ITeamParticipantRepository _teamParticipantsRepository;

        public TeamParticipantFilterQuery(ITeamParticipantRepository teamParticipantsRepository)
        {
            _teamParticipantsRepository = teamParticipantsRepository;
        }

        public async Task<TeamParticipantEntity[]> Query(CancellationToken ct, TeamParticipantFilterArgs args)
        {
            return await _teamParticipantsRepository.FilterTeamParticipants(ct, args);
        }
    }
}