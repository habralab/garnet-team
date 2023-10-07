using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Errors;
using Garnet.Teams.Events;

namespace Garnet.Teams.Application
{
    public class TeamJoinInviteCommand
    {
        private readonly ITeamUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;
        private readonly ITeamJoinInvitationRepository _joinInvitationRepository;
        private readonly IMessageBus _messageBus;
        public TeamJoinInviteCommand(
            ITeamJoinInvitationRepository joinInvitationRepository,
            ITeamParticipantRepository participantRepository,
            ITeamUserJoinRequestRepository userJoinRequestRepository,
            ITeamUserRepository userRepository,
            IMessageBus messageBus,
            ITeamRepository teamRepository)
        {
            _messageBus = messageBus;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _userJoinRequestRepository = userJoinRequestRepository;
            _joinInvitationRepository = joinInvitationRepository;
            _participantRepository = participantRepository;
        }

        public async Task<Result<TeamJoinInvitation>> InviteUserToTeam(CancellationToken ct, ICurrentUserProvider currentUserProvider, TeamJoinInviteArgs args)
        {
            var team = await _teamRepository.GetTeamById(ct, args.TeamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(args.TeamId));
            }

            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanInviteError());
            }

            var user = await _userRepository.GetUser(ct, args.UserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(args.UserId));
            }

            var userRequests = await _userJoinRequestRepository.GetAllUserJoinRequestsByTeam(ct, args.TeamId);
            if (userRequests.Any(x => x.UserId == args.UserId))
            {
                return Result.Fail(new TeamPendingUserJoinRequestError(args.UserId));
            }

            var teamParticipants = await _participantRepository.GetMembershipOfUser(ct, args.UserId);
            if (teamParticipants.Any(x => x.TeamId == args.TeamId))
            {
                return Result.Fail(new TeamUserIsAlreadyAParticipantError(args.UserId));
            }

            var filter = new TeamJoinInvitationFilterArgs(args.UserId, args.TeamId);
            var invitations = await _joinInvitationRepository.FilterInvitations(ct, filter);
            if (invitations.Count() > 0)
            {
                return Result.Fail(new TeamPendingJoinInvitationError(args.UserId));
            }

            var invitation = await _joinInvitationRepository.CreateInvitation(ct, args.UserId, args.TeamId);
            var @event = new TeamJoinInvitationCreatedEvent(args.UserId, args.TeamId);
            await _messageBus.Publish(@event);

            var invitationResult = new TeamJoinInvitation(invitation.Id, invitation.UserId, invitation.TeamId);
            return Result.Ok(invitationResult);
        }
    }
}