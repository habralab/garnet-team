using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;
using Garnet.Teams.Application.TeamUserJoinRequest.Notifications;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Commands
{
    public class TeamUserJoinRequestCancelCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamUserJoinRequestRepository _teamUserJoinRequestRepository;
        private readonly ITeamUserRepository _teamUserRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMessageBus _messageBus;
        public TeamUserJoinRequestCancelCommand(
            IMessageBus messageBus,
            ITeamRepository teamRepository,
            ITeamUserRepository teamUserRepository,
            ICurrentUserProvider currentUserProvider,
            ITeamUserJoinRequestRepository teamUserJoinRequestRepository)
        {
            _currentUserProvider = currentUserProvider;
            _teamUserJoinRequestRepository = teamUserJoinRequestRepository;
            _messageBus = messageBus;
            _teamRepository = teamRepository;
            _teamUserRepository = teamUserRepository;
        }

        public async Task<Result<TeamUserJoinRequestEntity>> Execute(CancellationToken ct, string userJoinRequestId)
        {
            var userJoinRequest = await _teamUserJoinRequestRepository.GetUserJoinRequestById(ct, userJoinRequestId);
            if (userJoinRequest is null)
            {
                return Result.Fail(new TeamUserJoinRequestNotFoundError(userJoinRequestId));
            }

            if (userJoinRequest.UserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new TeamUserJoinRequestOnlyAuthorCanCancelError());
            }

            await _teamUserJoinRequestRepository.DeleteUserJoinRequestById(ct, userJoinRequestId);

            var @event = userJoinRequest.ToCancelledEvent();
            await _messageBus.Publish(@event);

            var team = await _teamRepository.GetTeamById(ct, userJoinRequest.TeamId);
            var user = await _teamUserRepository.GetUser(ct, userJoinRequest.UserId);
            var notification = userJoinRequest.CreateTeamUserJoinRequestCancelNotification(team!, user!.Username);
            await _messageBus.Publish(notification);
            return Result.Ok(userJoinRequest);
        }
    }
}