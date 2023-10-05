
using FluentResults;
using Garnet.Common.Application;
using Garnet.Teams.Application.Errors;

namespace Garnet.Teams.Application
{
    public class TeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly TeamParticipantService _participantService;
        private readonly TeamUserService _userService;
        public TeamService(ITeamRepository teamRepository,
                           TeamParticipantService participantService,
                           TeamUserService userService)
        {
            _participantService = participantService;
            _teamRepository = teamRepository;
            _userService = userService;
        }

        public async Task<Result<Team>> CreateTeam(CancellationToken ct, string name, string description, string[] tags, ICurrentUserProvider currentUserProvider)
        {
            var existingUser = await _userService.GetUser(ct, currentUserProvider.UserId);
            if (existingUser.IsFailed)
            {
                return Result.Fail(existingUser.Errors);
            }

            var user = existingUser.Value;
            var team = await _teamRepository.CreateTeam(ct, name, description, user.Id, tags);
            await _participantService.CreateTeamParticipant(ct, user.Id, team.Id);

            return Result.Ok(team);
        }

        public async Task<Result<Team>> GetTeamById(CancellationToken ct, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);

            return team is null ? Result.Fail(new TeamNotFoundError(teamId)) : Result.Ok(team);
        }

        public async Task<Team[]> FilterTeams(CancellationToken ct, string? search, string[] tags, int skip, int take)
        {
            return await _teamRepository.FilterTeams(ct, search, tags, skip, take);
        }

        public async Task<Result<Team>> DeleteTeam(CancellationToken ct, string teamId, ICurrentUserProvider currentUserProvider)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanDeleteError());
            }

            await _teamRepository.DeleteTeam(ct, teamId);
            await _participantService.DeleteTeamParticipants(ct, teamId);

            return Result.Ok(team);
        }

        public async Task<Result<Team>> EditTeamDescription(CancellationToken ct, string teamId, string description, ICurrentUserProvider currentUserProvider)
        {
            var result = await GetTeamById(ct, teamId);
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }

            var team = result.Value;
            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanEditError());
            }

            team = await _teamRepository.EditTeamDescription(ct, teamId, description);

            return Result.Ok(team!);
        }

        public async Task<Result<Team>> EditTeamOwner(CancellationToken ct, string teamId, string newOwnerUserId, ICurrentUserProvider currentUserProvider)
        {
            var result = await GetTeamById(ct, teamId);
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }

            var team = result.Value;
            if (team.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanChangeOwnerError());
            }

            var existingUser = await _userService.GetUser(ct, newOwnerUserId);
            if (existingUser.IsFailed)
            {
                return Result.Fail(existingUser.Errors);
            }

            var userIsParticipant = await _participantService.EnsureUserIsTeamParticipant(ct, newOwnerUserId, teamId);
            if (userIsParticipant.IsFailed)
            {
                return Result.Fail(userIsParticipant.Errors);
            }

            team = await _teamRepository.EditTeamOwner(ct, teamId, newOwnerUserId);

            return Result.Ok(team!);
        }
    }
}