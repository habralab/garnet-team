using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.Team.Notifications;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Errors;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamEditOwnerCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly ITeamUserRepository _userRepository;
        private readonly IMessageBus _messageBus;

        public TeamEditOwnerCommand(
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            ITeamParticipantRepository participantRepository,
            ITeamUserRepository userRepository,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _participantRepository = participantRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamEntity>> Execute(CancellationToken ct, string teamId, string newOwnerUserId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team.OwnerUserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanChangeOwnerError());
            }

            var user = await _userRepository.GetUser(ct, newOwnerUserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(newOwnerUserId));
            }

            var userIsParticipant = await _participantRepository.GetParticipantsFromTeam(ct, teamId);
            if (!userIsParticipant.Any(x=> x.UserId == newOwnerUserId))
            {
                return Result.Fail(new TeamUserNotATeamParticipantError(newOwnerUserId));
            }

            team = await _teamRepository.EditTeamOwner(ct, teamId, newOwnerUserId);

            var @event = team!.ToUpdatedEvent();
            await _messageBus.Publish(@event);

            var notification = team!.CreateTeamInviteNotification(user.Username);
            await _messageBus.Publish(notification);
            return Result.Ok(team!);
        }
    }
}