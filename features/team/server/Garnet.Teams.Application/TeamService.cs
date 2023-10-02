
using FluentResults;
using Garnet.Common.Application;
using Garnet.Teams.Application.Errors;

namespace Garnet.Teams.Application
{
    public class TeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _teamParticipantsRepository;
        private readonly TeamUserService _userService;
        public TeamService(ITeamRepository teamRepository,
                           ITeamParticipantRepository teamParticipantsRepository,
                           TeamUserService userService)
        {
            _teamParticipantsRepository = teamParticipantsRepository;
            _teamRepository = teamRepository;
            _userService = userService;
        }

        public async Task<Result<Team>> CreateTeam(CancellationToken ct, string name, string description, string[] tags, ICurrentUserProvider currentUserProvider)
        {
            var user = await _userService.GetUser(ct, currentUserProvider.UserId);
            if (user is null)
            {
                return Result.Fail($"Пользователь с идентификатором '{currentUserProvider.UserId}' не найден");
            }

            var team = await _teamRepository.CreateTeam(ct, name, description, user.UserId, tags);
            await _teamParticipantsRepository.CreateTeamParticipant(ct, currentUserProvider.UserId, team.Id);
            return Result.Ok(team);
        }

        public async Task<Team?> GetTeamById(CancellationToken ct, string teamId)
        {
            return await _teamRepository.GetTeamById(ct, teamId);
        }

        public async Task<TeamParticipant[]> GetParticipantsFromTeam(CancellationToken ct, string teamId)
        {
            return await _teamParticipantsRepository.GetParticipantsFromTeam(ct, teamId);
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
            await _teamParticipantsRepository.DeleteTeamParticipants(ct, teamId);

            return Result.Ok(team);
        }

        public async Task<Result<Team>> EditTeamDescription(CancellationToken ct, string teamId, string description, ICurrentUserProvider currentUserProvider)
        {
            var team = await GetTeamById(ct, teamId);

            if (team is null)
            {
                return Result.Fail($"Команда с идентификатором '{teamId}' не найдена");
            }

            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail("Команду может отредактировать только ее владелец");
            }

            team = await _teamRepository.EditTeamDescription(ct, teamId, description);

            return Result.Ok(team!);
        }

        public async Task<Result<Team>> EditTeamOwner(CancellationToken ct, string teamId, string newOwnerUserId, ICurrentUserProvider currentUserProvider)
        {
            var team = await GetTeamById(ct, teamId);

            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanChangeOwnerError());
            }

            var user = await _userService.GetUser(ct, newOwnerUserId);
            if (user is null)
            {
                return Result.Fail($"Пользователь с идентификатором '{newOwnerUserId}' не найден");
            }

            var userTeams = await _teamParticipantsRepository.GetMembershipOfUser(ct, newOwnerUserId);
            if (!userTeams.Any(x => x.TeamId == teamId))
            {
                return Result.Fail(new TeamOnlyParticipantCanBeOwnerError(newOwnerUserId));
            }

            team = await _teamRepository.EditTeamOwner(ct, teamId, newOwnerUserId);

            return Result.Ok(team!);
        }
    }
}