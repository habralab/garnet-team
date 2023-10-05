using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Errors;
using Garnet.Teams.Events;

namespace Garnet.Teams.Application
{
    public class TeamMembershipService
    {
        private readonly ITeamUserJoinRequestRepository _membershipRepository;
        private readonly TeamParticipantService _participantService;
        private readonly TeamUserService _userService;
        private readonly TeamService _teamService;
        private readonly IMessageBus _messageBus;

        public TeamMembershipService(
            IMessageBus messageBus,
            ITeamUserJoinRequestRepository membershipRepository,
            TeamParticipantService participantService,
            TeamService teamService,
            TeamUserService userService)
        {
            _membershipRepository = membershipRepository;
            _participantService = participantService;
            _userService = userService;
            _teamService = teamService;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamUserJoinRequest>> CreateJoinRequestByUser(CancellationToken ct, string teamId, ICurrentUserProvider currentUserProvider)
        {
            var findTeam = await _teamService.GetTeamById(ct, teamId);
            if (findTeam.IsFailed)
            {
                return Result.Fail(findTeam.Errors);
            }

            var existingUser = await _userService.GetUser(ct, currentUserProvider.UserId);
            if (existingUser.IsFailed)
            {
                return Result.Fail(existingUser.Errors);
            }

            var user = existingUser.Value;
            var userRequest = await _membershipRepository.GetAllUserJoinRequestsByTeam(ct, teamId);
            if (userRequest.Any(x => x.UserId == user.Id))
            {
                return Result.Fail(new TeamPendingUserJoinRequestError(user.Id));
            }

            var participant = await _participantService.EnsureUserIsTeamParticipant(ct, user.Id, teamId);
            if (participant.IsSuccess)
            {
                return Result.Fail(new TeamUserIsAlreadyAParticipantError(user.Id));
            }

            var request = await _membershipRepository.CreateJoinRequestByUser(ct, user.Id, teamId);

            var @event = new TeamUserJoinRequestCreatedEvent(user.Id, teamId);
            await _messageBus.Publish(@event);

            return Result.Ok(request);
        }

        public async Task<Result<TeamUserJoinRequest[]>> GetAllUserJoinRequestByTeam(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId)
        {
            var findTeam = await _teamService.GetTeamById(ct, teamId);
            if (findTeam.IsFailed)
            {
                return Result.Fail(findTeam.Errors);
            }

            var team = findTeam.Value;
            if (team.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamUserJoinRequestOnlyOwnerCanSeeError());
            }

            var userJoinRequests = await _membershipRepository.GetAllUserJoinRequestsByTeam(ct, teamId);
            return Result.Ok(userJoinRequests);
        }
    }
}