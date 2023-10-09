
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

        public async Task<Result<TeamParticipantEntity>> CreateTeamParticipant(CancellationToken ct, string userId, string teamId)
        {
            var existingUser = await _userService.GetUser(ct, userId);
            if (existingUser.IsFailed)
            {
                return Result.Fail(existingUser.Errors);
            }

            var user = existingUser.Value;
            var participant = await _teamParticipantsRepository.CreateTeamParticipant(ct, userId, user.Username, teamId);
            return Result.Ok(participant);
        }

        public async Task<TeamParticipantEntity[]> DeleteTeamParticipants(CancellationToken ct, string teamId)
        {
            return await _teamParticipantsRepository.DeleteTeamParticipants(ct, teamId);
        }

        public async Task<TeamParticipantEntity[]> GetMembershipOfUser(CancellationToken ct, string userId)
        {
            return await _teamParticipantsRepository.GetMembershipOfUser(ct, userId);
        }

        public async Task<TeamParticipantEntity[]> GetParticipantsFromTeam(CancellationToken ct, string teamId)
        {
            return await _teamParticipantsRepository.GetParticipantsFromTeam(ct, teamId);
        }

        public async Task<Result> EnsureUserIsTeamParticipant(CancellationToken ct, string userId, string teamId)
        {
            var userTeams = await GetMembershipOfUser(ct, userId);
            if (!userTeams.Any(x => x.TeamId == teamId))
            {
                return Result.Fail(new TeamUserNotATeamParticipantError(userId));
            }

            return Result.Ok();
        }

        public async Task<TeamParticipantEntity[]> FindTeamParticipantByUsername(CancellationToken ct, string teamId, string? query, int take, int skip)
        {
            var filter = new TeamUserFilterArgs(query?.Trim(), take, skip);
            return await _teamParticipantsRepository.FilterTeamParticipants(ct, filter);
        }

        public async Task UpdateTeamParticipant(CancellationToken ct, string userId, TeamParticipantUpdateArgs update)
        {
            await _teamParticipantsRepository.UpdateTeamParticipant(ct, userId, update);
        }
    }
}