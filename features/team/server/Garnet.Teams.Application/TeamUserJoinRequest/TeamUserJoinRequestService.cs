using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Errors;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUserJoinRequest.Entities;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;
using Garnet.Teams.Events;
using Garnet.Teams.Events.TeamUserJoinRequest;

namespace Garnet.Teams.Application.TeamUserJoinRequest
{
    public class TeamUserJoinRequestService
    {
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;
        private readonly TeamParticipantService _participantService;
        private readonly TeamUserService _userService;
        private readonly TeamService _teamService;
        private readonly IMessageBus _messageBus;

        public TeamUserJoinRequestService(
            IMessageBus messageBus,
            ITeamUserJoinRequestRepository userJoinRequestRepository,
            TeamParticipantService participantService,
            TeamService teamService,
            TeamUserService userService)
        {
            _userJoinRequestRepository = userJoinRequestRepository;
            _participantService = participantService;
            _userService = userService;
            _teamService = teamService;
            _messageBus = messageBus;
        }

        

        public async Task<Result> EnsureNoPendingUserJoinRequest(CancellationToken ct, string userId, string teamId)
        {
            var userRequest = await _userJoinRequestRepository.GetAllUserJoinRequestsByTeam(ct, teamId);
            if (userRequest.Any(x => x.UserId == userId))
            {
                return Result.Fail(new TeamPendingUserJoinRequestError(userId));
            }

            return Result.Ok();
        }

        public async Task<Result<TeamUserJoinRequestEntity[]>> GetAllUserJoinRequestByTeam(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId)
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

            var userJoinRequests = await _userJoinRequestRepository.GetAllUserJoinRequestsByTeam(ct, teamId);
            return Result.Ok(userJoinRequests);
        }

        public async Task<Result<TeamUserJoinRequestEntity>> UserJoinRequestDecide(CancellationToken ct, ICurrentUserProvider currentUserProvider, string userJoinRequestId, bool isApproved)
        {
            var userJoinRequest = await _userJoinRequestRepository.GetUserJoinRequestById(ct, userJoinRequestId);
            if (userJoinRequest is null)
            {
                return Result.Fail(new TeamUserJoinRequestNotFoundError(userJoinRequestId));
            }

            var findTeam = await _teamService.GetTeamById(ct, userJoinRequest!.TeamId);
            if (findTeam.IsFailed)
            {
                return Result.Fail(findTeam.Errors);
            }

            var team = findTeam.Value;
            if (team.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamUserJoinRequestOnlyOwnerCanDecideError());
            }

            if (isApproved)
            {
                var participantCreated = await _participantService.CreateTeamParticipant(ct, userJoinRequest.UserId, userJoinRequest.TeamId);
                if (participantCreated.IsFailed)
                {
                    return Result.Fail(participantCreated.Errors);
                }
            }
            await _userJoinRequestRepository.DeleteUserJoinRequestById(ct, userJoinRequestId);

            var @event = new TeamUserJoinRequestDecidedEvent(userJoinRequest.Id, userJoinRequest.UserId, userJoinRequest.TeamId, isApproved);
            await _messageBus.Publish(@event);
            return Result.Ok(userJoinRequest);
        }
    }
}