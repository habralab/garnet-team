using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamJoinInvitation.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Commands
{
    public class TeamJoinInvitationCancelCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamJoinInvitationRepository _teamJoinInvitationRepository;
        private readonly IMessageBus _messageBus;

        public TeamJoinInvitationCancelCommand(
            IMessageBus messageBus,
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            ITeamJoinInvitationRepository teamJoinInvitationRepository)
        {
            _currentUserProvider = currentUserProvider;
            _teamRepository = teamRepository;
            _teamJoinInvitationRepository = teamJoinInvitationRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamJoinInvitationEntity>> Execute(CancellationToken ct, string joinInvitationId)
        {
            var invitation = await _teamJoinInvitationRepository.GetById(ct, joinInvitationId);
            if (invitation is null)
            {
                return Result.Fail(new TeamJoinInvitationNotFoundError(joinInvitationId));
            }

            var team = await _teamRepository.GetTeamById(ct, invitation.TeamId);
            if (team!.OwnerUserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamJoinInvitationOnlyOwnerCanCancelError());
            }

            await _teamJoinInvitationRepository.DeleteInvitationById(ct, joinInvitationId);

            var @event = invitation.ToCancelledEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(invitation);
        }
    }
}