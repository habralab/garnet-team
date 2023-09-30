
using FluentResults;
using Garnet.Common.Application;

namespace Garnet.Teams.Application
{
    public class TeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _teamParticipantsRepository;
        public TeamService(ITeamRepository teamRepository, ITeamParticipantRepository teamParticipantsRepository)
        {
            _teamParticipantsRepository = teamParticipantsRepository;
            _teamRepository = teamRepository;
        }

        public async Task<Team> CreateTeam(CancellationToken ct, string name, string description, string[] tags, ICurrentUserProvider currentUserProvider)
        {
            var team = await _teamRepository.CreateTeam(ct, name, description, currentUserProvider.UserId, tags);
            await _teamParticipantsRepository.CreateTeamParticipant(ct, currentUserProvider.UserId, team.Id);
            return team;
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
                return Result.Fail($"Команда с идентификатором '{teamId}' не найдена");
            }

            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail("Команду может удалить только ее владелец");
            }

            await _teamRepository.DeleteTeam(ct, teamId);
            await _teamParticipantsRepository.DeleteTeamParticipants(ct, teamId);

            return Result.Ok(team);
        }
    }
}