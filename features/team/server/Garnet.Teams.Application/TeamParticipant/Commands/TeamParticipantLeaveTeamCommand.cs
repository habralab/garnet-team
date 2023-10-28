using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamParticipant.Errors;
using Garnet.Teams.Application.TeamParticipant.Notifications;
using Garnet.Teams.Application.TeamUser;

namespace Garnet.Teams.Application.TeamParticipant.Commands
{
    public class TeamParticipantLeaveTeamCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _teamParticipantRepository;
        private readonly ITeamUserRepository _teamUserRepository;
        private readonly IMessageBus _messageBus;

        public TeamParticipantLeaveTeamCommand(
            IMessageBus messageBus,
            ITeamUserRepository teamUserRepository,
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            ITeamParticipantRepository teamParticipantRepository)
        {
            _currentUserProvider = currentUserProvider;
            _messageBus = messageBus;
            _teamUserRepository = teamUserRepository;
            _teamRepository = teamRepository;
            _teamParticipantRepository = teamParticipantRepository;
        }

        public async Task<Result<TeamParticipantEntity>> Execute(CancellationToken ct, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team.OwnerUserId == _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOwnerCanNotLeaveTeamError());
            }

            var membership = await _teamParticipantRepository.IsParticipantInTeam(ct, _currentUserProvider.UserId, teamId);
            if (membership is null)
            {
                return Result.Fail(new TeamUserNotATeamParticipantError(_currentUserProvider.UserId));
            }

            await _teamParticipantRepository.DeleteParticipantById(ct, membership.Id);

            var @event = membership.ToLeftTeamEvent();
            await _messageBus.Publish(@event);

            var user = await _teamUserRepository.GetUser(ct, membership.UserId);
            var notification = membership.CreateParticipantLeaveTeamNotification(team, user!.Username);
            await _messageBus.Publish(notification);
            return Result.Ok(membership);
        }
    }
}