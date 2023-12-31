using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamJoinProjectRequest.Errors;

namespace Garnet.Teams.Application.TeamJoinProjectRequest.Commands
{
    public class TeamJoinProjectRequestCreateCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;

        public TeamJoinProjectRequestCreateCommand(
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            ITeamJoinProjectRequestRepository joinProjectRequestRepository,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _teamRepository = teamRepository;
            _joinProjectRequestRepository = joinProjectRequestRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamJoinProjectRequestEntity>> Execute(CancellationToken ct, string teamId, string projectId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team.OwnerUserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanRequestJoiningProjectError());
            }

            var joinProjectRequests = await _joinProjectRequestRepository.GetJoinProjectRequestsByTeam(ct, teamId);
            if (joinProjectRequests.Any(x => x.ProjectId == projectId))
            {
                return Result.Fail(new TeamPendingJoinProjectRequestError(teamId));
            }

            var joinProjectRequest = await _joinProjectRequestRepository.CreateJoinProjectRequest(ct, teamId, projectId);

            var @event = joinProjectRequest.ToCreatedEvent();
            await _messageBus.Publish(@event);

            return Result.Ok(joinProjectRequest);
        }
    }
}