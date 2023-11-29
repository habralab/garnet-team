using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamJoinInvitation.Errors;
using Garnet.Teams.Application.TeamJoinInvitation.Notifications;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Commands
{
    public class TeamJoinInvitationDecideCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamJoinInvitationRepository _teamJoinInvitationRepository;
        private readonly ITeamParticipantRepository _teamParticipantRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamUserRepository _teamUserRepository;
        private readonly IMessageBus _messageBus;

        public TeamJoinInvitationDecideCommand(
            ITeamParticipantRepository teamParticipantRepository,
            IMessageBus messageBus,
            ITeamUserRepository teamUserRepository,
            ITeamRepository teamRepository,
            ICurrentUserProvider currentUserProvider,
            ITeamJoinInvitationRepository teamJoinInvitationRepository)
        {
            _teamUserRepository = teamUserRepository;
            _teamRepository = teamRepository;
            _currentUserProvider = currentUserProvider;
            _teamJoinInvitationRepository = teamJoinInvitationRepository;
            _teamParticipantRepository = teamParticipantRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamJoinInvitationEntity>> Execute(CancellationToken ct, string joinInvitationId, bool isApproved)
        {
            var invitation = await _teamJoinInvitationRepository.GetById(ct, joinInvitationId);
            if (invitation is null)
            {
                return Result.Fail(new TeamJoinInvitationNotFoundError(joinInvitationId));
            }

            var team = await _teamRepository.GetTeamById(ct, invitation.TeamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(invitation.TeamId));
            }

            if (invitation.UserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamJoinInvitationOnlyInvitedUserCanDecideError());
            }

            var user = await _teamUserRepository.GetUser(ct, invitation.UserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(invitation.UserId));
            }

            if (isApproved)
            {
                var args = new TeamParticipantCreateArgs(user.Id, user.Username, user.AvatarUrl, team.Id);
                await _teamParticipantRepository.CreateTeamParticipant(ct, args);
            }
            await _teamJoinInvitationRepository.DeleteInvitationById(ct, joinInvitationId);

            var @event = invitation.ToDecidedEvent(isApproved);
            await _messageBus.Publish(@event);

            var notificationForDelete = invitation.DeleteTeamInviteNotification();
            await _messageBus.Publish(notificationForDelete);

            var notification = invitation.CreateTeamInviteDecideNotification(team, user, isApproved);
            await _messageBus.Publish(notification);
            return Result.Ok(invitation);
        }
    }
}