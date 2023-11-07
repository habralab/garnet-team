namespace Garnet.Teams.Application.TeamParticipant.Queries
{
    public class TeamParticipantListByTeamsQuery
    {
        private readonly ITeamParticipantRepository _teamParticipantRepository;
        public TeamParticipantListByTeamsQuery(ITeamParticipantRepository teamParticipantRepository)
        {
            _teamParticipantRepository = teamParticipantRepository;
        }

        public async Task<Dictionary<string, TeamParticipantEntity[]>> Query(CancellationToken ct, string[] teamIds)
        {
            var participants = await _teamParticipantRepository.TeamParticipantListOfTeams(ct, teamIds);
            var participantsByTeams = participants.GroupBy(x => x.TeamId).ToDictionary(x => x.Key, y => y.ToArray());
            return participantsByTeams;
        }
    }
}