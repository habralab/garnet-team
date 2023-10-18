using FluentResults;
using Garnet.Common.Application;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamJoinInvitation.Args;
using Garnet.Teams.Application.TeamJoinInvitation.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Queries
{
    public class TeamJoinInvitationsShowQuery
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamJoinInvitationRepository _teamJoinInvitationRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        public TeamJoinInvitationsShowQuery(
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            ITeamJoinInvitationRepository teamJoinInvitationRepository)
        {
            _currentUserProvider = currentUserProvider;
            _teamRepository = teamRepository;
            _teamJoinInvitationRepository = teamJoinInvitationRepository;
        }

        public async Task<Result<TeamJoinInvitationEntity[]>> Query(CancellationToken ct, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamJoinInvitationOnlyOwnerCanSeeError());
            }

            var filter = new TeamJoinInvitationFilterArgs(null, teamId);
            var joinInvitations = await _teamJoinInvitationRepository.FilterInvitations(ct, filter);
            return Result.Ok(joinInvitations);
        }
    }
}