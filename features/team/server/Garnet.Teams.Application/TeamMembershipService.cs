using FluentResults;
using Garnet.Common.Application;
using Garnet.Teams.Application.Errors;

namespace Garnet.Teams.Application
{
    public class TeamMembershipService
    {
        private readonly ITeamMembershipRepository _membershipRepository;
        private readonly ITeamParticipantRepository _teamParticipantsRepository;
        private readonly TeamUserService _userService;

        public TeamMembershipService(
            ITeamMembershipRepository membershipRepository,
            ITeamParticipantRepository teamParticipantsRepository,
            TeamUserService userService)
        {
            _membershipRepository = membershipRepository;
            _teamParticipantsRepository = teamParticipantsRepository;
            _userService = userService;
        }

        public async Task<Result<TeamUserJoinRequest>> JoinRequestByUser(CancellationToken ct, string teamId, ICurrentUserProvider currentUserProvider)
        {
            var user = await _userService.GetUser(ct, currentUserProvider.UserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(currentUserProvider.UserId));
            }

            var userRequest = await _membershipRepository.GetAllUserJoinRequestByTeam(ct, teamId);
            if (userRequest.Any(x => x.UserId == user.UserId))
            {
                return Result.Fail(new TeamPendingUserJoinRequestError(user.UserId));
            }

            var participant = await _teamParticipantsRepository.GetParticipantsFromTeam(ct, teamId);
            if (participant.Any(x => x.UserId == user.UserId))
            {
                return Result.Fail(new TeamUserIsAlreadyAParticipantError(user.UserId));
            }

            var request = await _membershipRepository.CreateJoinRequestByUser(ct, user.UserId, teamId);
            return Result.Ok(request);
        }
    }
}