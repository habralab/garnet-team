
using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.Team.Errors;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Events;
using Garnet.Teams.Events.Team;

namespace Garnet.Teams.Application.Team
{
    public class TeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly TeamParticipantService _participantService;
        private readonly TeamUserService _userService;
        private readonly IMessageBus _messageBus;
        public TeamService(ITeamRepository teamRepository,
                            IMessageBus messageBus,
                           TeamParticipantService participantService,
                           TeamUserService userService)
        {
            _messageBus = messageBus;
            _participantService = participantService;
            _teamRepository = teamRepository;
            _userService = userService;
        }

        public async Task<Result<TeamEntity>> GetTeamById(CancellationToken ct, string teamId)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);

            return team is null ? Result.Fail(new TeamNotFoundError(teamId)) : Result.Ok(team);
        }

        public async Task<TeamEntity[]> FilterTeams(CancellationToken ct, string? search, string[] tags, int skip, int take)
        {
            return await _teamRepository.FilterTeams(ct, search, tags, skip, take);
        }

    }
}