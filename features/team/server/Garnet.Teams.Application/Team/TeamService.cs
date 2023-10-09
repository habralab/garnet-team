
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

        public async Task<Result<TeamEntity>> DeleteTeam(CancellationToken ct, string teamId, ICurrentUserProvider currentUserProvider)
        {
            var team = await _teamRepository.GetTeamById(ct, teamId);
            if (team is null)
            {
                return Result.Fail(new TeamNotFoundError(teamId));
            }

            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanDeleteError());
            }

            await _teamRepository.DeleteTeam(ct, teamId);
            await _participantService.DeleteTeamParticipants(ct, teamId);

            var @event = new TeamDeletedEvent(team.Id, team.Name, team.OwnerUserId, team.Description, team.Tags);
            await _messageBus.Publish(@event);
            return Result.Ok(team);
        }

        public async Task<Result<TeamEntity>> EditTeamDescription(CancellationToken ct, string teamId, string description, ICurrentUserProvider currentUserProvider)
        {
            var result = await GetTeamById(ct, teamId);
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }

            var team = result.Value;
            if (team!.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanEditError());
            }

            team = await _teamRepository.EditTeamDescription(ct, teamId, description);

            var @event = new TeamUpdatedEvent(team!.Id, team.Name, team.OwnerUserId, team.Description, team.Tags);
            await _messageBus.Publish(@event);
            return Result.Ok(team!);
        }

        public async Task<Result<TeamEntity>> EditTeamOwner(CancellationToken ct, string teamId, string newOwnerUserId, ICurrentUserProvider currentUserProvider)
        {
            var result = await GetTeamById(ct, teamId);
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }

            var team = result.Value;
            if (team.OwnerUserId != currentUserProvider.UserId)
            {
                return Result.Fail(new TeamOnlyOwnerCanChangeOwnerError());
            }

            var existingUser = await _userService.GetUser(ct, newOwnerUserId);
            if (existingUser.IsFailed)
            {
                return Result.Fail(existingUser.Errors);
            }

            var userIsParticipant = await _participantService.EnsureUserIsTeamParticipant(ct, newOwnerUserId, teamId);
            if (userIsParticipant.IsFailed)
            {
                return Result.Fail(userIsParticipant.Errors);
            }

            team = await _teamRepository.EditTeamOwner(ct, teamId, newOwnerUserId);

            var @event = new TeamUpdatedEvent(team!.Id, team.Name, team.OwnerUserId, team.Description, team.Tags);
            await _messageBus.Publish(@event);
            return Result.Ok(team!);
        }
    }
}