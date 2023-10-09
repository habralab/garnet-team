using FluentResults;
using Garnet.Common.Application;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamUserJoinRequest.Entities;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Queries
{
    public class TeamUserJoinRequestsShowQuery
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;

        public TeamUserJoinRequestsShowQuery(ITeamRepository teamRepository, ITeamUserJoinRequestRepository userJoinRequestRepository)
        {
            _teamRepository = teamRepository;
            _userJoinRequestRepository = userJoinRequestRepository;
        }

        public async Task<Result<TeamUserJoinRequestEntity[]>> Query(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamUserJoinRequestOnlyOwnerCanSeeError());
            }

            var userJoinRequests = await _userJoinRequestRepository.GetAllUserJoinRequestsByTeam(ct, teamId);
            return Result.Ok(userJoinRequests);
        }
    }
}