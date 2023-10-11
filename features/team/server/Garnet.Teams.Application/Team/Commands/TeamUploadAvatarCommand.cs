using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Common.Application.S3;
using Garnet.Teams.Application.Team.Errors;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamUploadAvatarCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IRemoteFileStorage _fileStorage;
        private readonly IMessageBus _messageBus;

        public TeamUploadAvatarCommand(
            IMessageBus messageBus,
            ITeamRepository teamRepository,
            IRemoteFileStorage fileStorage)
        {
            _messageBus = messageBus;
            _teamRepository = teamRepository;
            _fileStorage = fileStorage;
        }

        public async Task<Result<TeamEntity>> Execute(CancellationToken ct, ICurrentUserProvider currentUserProvider, string teamId, string? contentType, Stream imageStream)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanChangeAvatarError());
            }

            var avatarUrl = await _fileStorage.UploadFile($"avatars/{currentUserProvider.UserId}", contentType, imageStream);
            team = await _teamRepository.EditTeamAvatar(ct, teamId, avatarUrl);

            var @event = team!.ToUpdatedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(team!);
        }
    }
}