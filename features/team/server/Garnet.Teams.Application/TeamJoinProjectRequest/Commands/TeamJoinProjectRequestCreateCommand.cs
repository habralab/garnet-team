using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamJoinProjectRequest.Errors;
using Garnet.Teams.Events;
using Garnet.Teams.Events.TeamJoinProjectRequest;

namespace Garnet.Teams.Application.TeamJoinProjectRequest.Commands
{
    public class TeamJoinProjectRequestCreateCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;

        public TeamJoinProjectRequestCreateCommand(
            ITeamRepository teamRepository,
            ITeamJoinProjectRequestRepository joinProjectRequestRepository,
            IMessageBus messageBus)
        {
            _teamRepository = teamRepository;
            _joinProjectRequestRepository = joinProjectRequestRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamJoinProjectRequestEntity>> SendJoinProjectRequest(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId, string projectId)
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

            var @event = joinProjectRequest.ToCreatedEvent();
            await _messageBus.Publish(@event);

            return Result.Ok(joinProjectRequest);
        }
    }
}