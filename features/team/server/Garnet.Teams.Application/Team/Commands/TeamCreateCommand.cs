using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Args;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Errors;
using Garnet.Teams.Events.Team;

namespace Garnet.Teams.Application.Team.Commands
{
    public class TeamCreateCommand
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamUserRepository _usersRepository;
        private readonly ITeamParticipantRepository _participantRepository;
        private readonly IMessageBus _messageBus;

        public TeamCreateCommand(
            ITeamRepository teamRepository,
            ITeamUserRepository usersRepository,
            ITeamParticipantRepository participantRepository,
            IMessageBus messageBus)
        {
            _usersRepository = usersRepository;
            _teamRepository = teamRepository;
            _participantRepository = participantRepository;
            _messageBus = messageBus;
        }

        public async Task<Result<TeamEntity>> Execute(CancellationToken ct, TeamCreateArgs args)
        {
            var user = await _usersRepository.GetUser(ct, args.OwnerUserId);
            if (user is null)
            {
                return Result.Fail(new TeamUserNotFoundError(args.OwnerUserId));
            }

            var team = await _teamRepository.CreateTeam(ct, args);
            await _participantRepository.CreateTeamParticipant(ct, user.Id, user.Username, team.Id);

            var @event = team.ToCreatedEvent();
            await _messageBus.Publish(@event);
            return Result.Ok(team);
        }
    }
}