using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamJoinInvitation.Args;
using Garnet.Teams.Application.TeamJoinInvitation.Errors;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Errors;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;
using Garnet.Teams.Application.TeamUserJoinRequest;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;
using Garnet.Teams.Events;
using Garnet.Teams.Events.TeamJoinInvitation;

namespace Garnet.Teams.Application.TeamJoinInvitation.Commands
{
    public class TeamJoinInviteCommand
    {
        private readonly ITeamUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly ITeamUserJoinRequestRepository _userJoinRequestRepository;
        private readonly ITeamJoinInvitationRepository _joinInvitationRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMessageBus _messageBus;
        public TeamJoinInviteCommand(
            ICurrentUserProvider currentUserProvider,
            ITeamJoinInvitationRepository joinInvitationRepository,
            ITeamParticipantRepository participantRepository,
            ITeamUserJoinRequestRepository userJoinRequestRepository,
            ITeamUserRepository userRepository,
            IMessageBus messageBus,
            ITeamRepository teamRepository)
        {
            _currentUserProvider = currentUserProvider;
            _messageBus = messageBus;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _userJoinRequestRepository = userJoinRequestRepository;
            _joinInvitationRepository = joinInvitationRepository;
            _participantRepository = participantRepository;
        }

        public async Task<Result<TeamJoinInvitationEntity>> Execute(CancellationToken ct, TeamJoinInviteArgs args)
        {
            var team = await _teamRepository.GetTeamById(ct, args.TeamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(args.TeamId));
            }

            if (team!.OwnerUserId != _currentUserProvider.UserId)
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

            var membership = await _participantRepository.IsParticipantInTeam(ct, args.UserId, args.TeamId);
            if (membership is not null)
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
            var @event = invitation.ToCreatedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(invitation);
        }
    }
}