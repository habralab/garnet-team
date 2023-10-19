using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamParticipant;

namespace Garnet.Teams.Application.TeamJoinInvitation.Commands
{
    public class TeamJoinInvitationDecideCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamJoinInvitationRepository _teamJoinInvitationRepository;
        private readonly ITeamParticipantRepository _teamParticipantRepository;
        private readonly IMessageBus _messageBus;

        public TeamJoinInvitationDecideCommand(
            ITeamParticipantRepository teamParticipantRepository,
            IMessageBus messageBus,
            ICurrentUserProvider currentUserProvider,
            ITeamJoinInvitationRepository teamJoinInvitationRepository)
        {
            _currentUserProvider = currentUserProvider;
            _teamJoinInvitationRepository = teamJoinInvitationRepository;
            _teamParticipantRepository = teamParticipantRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamJoinInvitationEntity>> Execute(CancellationToken ct, string joinInvitationId, bool isApproved)
        {
            return null;
        }
    }
}