using FluentResults;
using Garnet.Common.Application;
using Garnet.Teams.Application.Errors;

namespace Garnet.Teams.Application
{
    public class TeamMembershipService
    {
        private readonly ITeamUserJoinRequestRepository _membershipRepository;
        private readonly ITeamParticipantRepository _teamParticipantsRepository;
        private readonly TeamUserService _userService;
        private readonly TeamService _teamService;

        public TeamMembershipService(
            ITeamUserJoinRequestRepository membershipRepository,
            ITeamParticipantRepository teamParticipantsRepository,
            TeamService teamService,
            TeamUserService userService)
        {
            _membershipRepository = membershipRepository;
            _teamParticipantsRepository = teamParticipantsRepository;
            _userService = userService;
            _teamService = teamService;
        }

        public async Task<Result<TeamUserJoinRequest>> JoinRequestByUser(CancellationToken ct, string teamId, ICurrentUserProvider currentUserProvider)
        {
            var findTeam = await _teamService.GetTeamById(ct, teamId);
            if (findTeam.IsFailed)
            {
                return Result.Fail(findTeam.Errors);
            }

            var user = await _userService.GetUser(ct, currentUserProvider.UserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(currentUserProvider.UserId));
            }

            var userRequest = await _membershipRepository.GetAllUserJoinRequestByTeam(ct, teamId);
            if (userRequest.Any(x => x.UserId == user.Id))
            {
                return Result.Fail(new TeamPendingUserJoinRequestError(user.Id));
            }

            var participant = await _teamParticipantsRepository.GetParticipantsFromTeam(ct, teamId);
            if (participant.Any(x => x.UserId == user.Id))
            {
                return Result.Fail(new TeamUserIsAlreadyAParticipantError(user.Id));
            }

            var request = await _membershipRepository.CreateJoinRequestByUser(ct, user.Id, teamId);
            return Result.Ok(request);
        }
    }
}