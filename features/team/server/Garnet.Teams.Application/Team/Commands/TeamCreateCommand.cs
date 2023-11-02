using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Common.Application.S3;
using Garnet.Teams.Application.Team.Args;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Args;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamCreateCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITeamUserRepository _usersRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly IMessageBus _messageBus;
        private readonly IRemoteFileStorage _fileStorage;

        public TeamCreateCommand(
            ICurrentUserProvider currentUserProvider,
            ITeamRepository teamRepository,
            ITeamUserRepository usersRepository,
            ITeamParticipantRepository participantRepository,
            IRemoteFileStorage fileStorage,
            IMessageBus messageBus)
        {
            _currentUserProvider = currentUserProvider;
            _usersRepository = usersRepository;
            _teamRepository = teamRepository;
            _participantRepository = participantRepository;
            _messageBus = messageBus;
            _fileStorage = fileStorage;
        }

        public async Task<Result<TeamEntity>> Execute(CancellationToken ct, TeamCreateArgs args)
        {
            var currentUserId = _currentUserProvider.UserId;

            var user = await _usersRepository.GetUser(ct, currentUserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(currentUserId));
            }

            args = args with { Name = args.Name.Trim() };
            if (string.IsNullOrWhiteSpace(args.Name))
            {
                return Result.Fail(new TeamNameCanNotBeEmptyError());
            }

            var team = await _teamRepository.CreateTeam(ct, currentUserId, args);
            var participantArgs = new TeamParticipantCreateArgs(user.Id, user.Username, user.AvatarUrl, team.Id);
            await _participantRepository.CreateTeamParticipant(ct, participantArgs);
            await _teamRepository.IncreaseParticipantCount(ct, team.Id, user.AvatarUrl);

            if (args.Avatar is not null)
            {
                var avatarUrl = await _fileStorage.UploadFile($"avatars/team/{team.Id}", args.Avatar.ContentType, args.Avatar.Stream);
                team = await _teamRepository.EditTeamAvatar(ct, team.Id, avatarUrl);
            }

            var @event = team!.ToCreatedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(team!);
        }
    }
}