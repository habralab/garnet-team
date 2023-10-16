using Garnet.Teams.Application.Team.Args;
using Garnet.Teams.Application.TeamParticipant;

namespace Garnet.Teams.Application.Team.Queries
{
    public class TeamsListByUserQuery
    {
        private readonly ITeamParticipantRepository _teamParticipantRepository;
        private readonly ITeamRepository _teamRepository;

        public TeamsListByUserQuery(
            ITeamRepository teamRepository,
            ITeamParticipantRepository teamParticipantRepository)
        {
            _teamRepository = teamRepository;
            _teamParticipantRepository = teamParticipantRepository;
        }

        public async Task<TeamEntity[]> Query(CancellationToken ct, string userId, TeamsListArgs args)
        {
            var userMemberships = await _teamParticipantRepository.GetMembershipOfUser(ct, userId);
            var teamIds = userMemberships.Select(x => x.TeamId).ToArray();
            return await _teamRepository.GetTeamsById(ct, teamIds, args);
        }
    }
}