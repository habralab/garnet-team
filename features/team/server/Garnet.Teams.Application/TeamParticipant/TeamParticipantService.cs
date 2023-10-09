
using FluentResults;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamParticipant.Entities;
using Garnet.Teams.Application.TeamParticipant.Errors;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Args;

namespace Garnet.Teams.Application.TeamParticipant
{
    public class TeamParticipantService
    {
        private readonly ITeamParticipantRepository _teamParticipantsRepository;
        private readonly TeamUserService _userService;

        public TeamParticipantService(
            ITeamParticipantRepository teamParticipantsRepository,
            TeamUserService userService)
        {
            _teamParticipantsRepository = teamParticipantsRepository;
            _userService = userService;
        }

        public async Task<TeamParticipantEntity[]> FindTeamParticipantByUsername(CancellationToken ct, string teamId, string? query, int take, int skip)
        {
            var filter = new TeamUserFilterArgs(query?.Trim(), take, skip);
            return await _teamParticipantsRepository.FilterTeamParticipants(ct, filter);
        }
    }
}