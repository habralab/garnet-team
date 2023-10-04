
using FluentResults;
using Garnet.Teams.Application.Errors;

namespace Garnet.Teams.Application
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

        public async Task<Result<TeamParticipant>> CreateTeamParticipant(CancellationToken ct, string userId, string teamId)
        {
            var user = await _userService.GetUser(ct, userId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(userId));
            }

            var participant = await _teamParticipantsRepository.CreateTeamParticipant(ct, userId, teamId);
            return Result.Ok(participant);
        }

        public async Task<TeamParticipant[]> DeleteTeamParticipants(CancellationToken ct, string teamId)
        {
            return await _teamParticipantsRepository.DeleteTeamParticipants(ct, teamId);
        }

        public async Task<TeamParticipant[]> GetMembershipOfUser(CancellationToken ct, string userId)
        {
            return await _teamParticipantsRepository.GetMembershipOfUser(ct, userId);
        }

        public async Task<TeamParticipant[]> GetParticipantsFromTeam(CancellationToken ct, string teamId)
        {
            return await _teamParticipantsRepository.GetParticipantsFromTeam(ct, teamId);
        }
    }
}