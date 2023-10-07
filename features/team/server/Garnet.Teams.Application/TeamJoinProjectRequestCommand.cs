using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Errors;
using Garnet.Teams.Events;

namespace Garnet.Teams.Application
{
    public class TeamJoinProjectRequestCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;

        public TeamJoinProjectRequestCommand(
            ITeamRepository teamRepository,
            ITeamJoinProjectRequestRepository joinProjectRequestRepository,
            IMessageBus messageBus)
        {
            _teamRepository = teamRepository;
            _joinProjectRequestRepository = joinProjectRequestRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamJoinProjectRequest>> SendJoinProjectRequest(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId, string projectId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanRequestJoiningProjectError());
            }

            var joinProjectRequests = await _joinProjectRequestRepository.GetJoinProjectRequestsByTeam(ct, teamId);
            if (joinProjectRequests.Any(x => x.ProjectId == projectId))
            {
                return Result.Fail(new TeamPendingJoinProjectRequestError(teamId));
            }

            var joinProjectRequest = await _joinProjectRequestRepository.CreateJoinProjectRequest(ct, teamId, projectId);

            var @event = new TeamJoinProjectRequestCreatedEvent(projectId, teamId);
            await _messageBus.Publish(@event);

            return Result.Ok(joinProjectRequest);
        }
    }
}