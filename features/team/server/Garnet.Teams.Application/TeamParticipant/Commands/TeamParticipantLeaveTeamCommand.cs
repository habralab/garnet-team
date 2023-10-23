using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamParticipant.Errors;

namespace Garnet.Teams.Application.TeamParticipant.Commands
{
    public class TeamParticipantLeaveTeamCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamParticipantRepository _teamParticipantRepository;
        private readonly IMessageBus _messageBus;

        public TeamParticipantLeaveTeamCommand(
            IMessageBus messageBus,
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            ITeamParticipantRepository teamParticipantRepository)
        {
            _currentUserProvider = currentUserProvider;
            _messageBus = messageBus;
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

            var userMemberships = await _teamParticipantRepository.GetMembershipOfUser(ct, _currentUserProvider.UserId);
            var teamMembership = userMemberships.FirstOrDefault(x => x.TeamId == teamId);
            if (teamMembership is null)
            {
                return Result.Fail(new TeamUserNotATeamParticipantError(_currentUserProvider.UserId));
            }

            await _teamParticipantRepository.DeleteParticipantById(ct, teamMembership.Id);

            var @event = teamMembership.ToLeftTeamEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(teamMembership);
        }
    }
}