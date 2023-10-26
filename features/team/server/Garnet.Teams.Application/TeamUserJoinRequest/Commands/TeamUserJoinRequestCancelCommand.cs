using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamUserJoinRequest.Errors;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Commands
{
    public class TeamUserJoinRequestCancelCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamUserJoinRequestRepository _teamUserJoinRequestRepository;
        private readonly IMessageBus _messageBus;
        public TeamUserJoinRequestCancelCommand(
            IMessageBus messageBus,
            ICurrentUserProvider currentUserProvider,
            ITeamUserJoinRequestRepository teamUserJoinRequestRepository)
        {
            _currentUserProvider = currentUserProvider;
            _teamUserJoinRequestRepository = teamUserJoinRequestRepository;
            _messageBus = messageBus;
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
            return Result.Ok(userJoinRequest);
        }
    }
}