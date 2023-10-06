using FluentResults;
using Garnet.Common.Application;

namespace Garnet.Teams.Application
{
    public class TeamDeleteUnitOfWork
    {
        private readonly TeamService _teamService;
        private readonly TeamParticipantService _participantService;
        private readonly TeamUserJoinRequestService _userJoinRequestService;

        public TeamDeleteUnitOfWork(
            TeamService teamService,
            TeamParticipantService participantService,
            TeamUserJoinRequestService userJoinRequestService)
        {
            _teamService = teamService;
            _participantService = participantService;
            _userJoinRequestService = userJoinRequestService;
        }

        public async Task<Result<Team>> DeleteTeam(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId)
        {
            var deleteTeam = await _teamService.DeleteTeam(ct, teamId, currentUserProvider);

            if (deleteTeam.IsFailed)
            {
                return Result.Fail(deleteTeam.Errors);
            }

            var team = deleteTeam.Value;
            await _participantService.DeleteTeamParticipants(ct, team.Id);
            await _userJoinRequestService.DeleteUserJoinRequestsByTeam(ct, team.Id);

            return Result.Ok(team);
        }
    }
}