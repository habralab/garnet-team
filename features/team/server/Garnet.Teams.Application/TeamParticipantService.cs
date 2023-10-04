
using FluentResults;
using Garnet.Teams.Application.Errors;

namespace Garnet.Teams.Application
{
    public class TeamParticipantService
    {
        private readonly ITeamParticipantRepository _teamParticipantsRepository;
        private readonly TeamUserService _userService;

        public TeamParticipantService(
            ITeamParticipantRepository teamParticipantsRepository,
            TeamUserService userService)
        {
            _teamParticipantsRepository = teamParticipantsRepository;
            _userService = userService;
        }

        public async Task<Result<TeamParticipant>> CreateTeamParticipant(CancellationToken ct, string userId, string teamId)
        {
            var userExists = await _userService.GetUser(ct, userId);
            if (userExists.IsFailed)
            {
                return Result.Fail(userExists.Errors);
            }

            var participant = await _teamParticipantsRepository.CreateTeamParticipant(ct, userId, teamId);
            return Result.Ok(participant);
        }

        public async Task<TeamParticipant[]> DeleteTeamParticipants(CancellationToken ct, string teamId)
        {
            return await _teamParticipantsRepository.DeleteTeamParticipants(ct, teamId);
        }

        public async Task<TeamParticipant[]> GetMembershipOfUser(CancellationToken ct, string userId)
        {
            return await _teamParticipantsRepository.GetMembershipOfUser(ct, userId);
        }

        public async Task<TeamParticipant[]> GetParticipantsFromTeam(CancellationToken ct, string teamId)
        {
            return await _teamParticipantsRepository.GetParticipantsFromTeam(ct, teamId);
        }

        public async Task<Result> UserIsTeamParticipant(CancellationToken ct, string userId, string teamId)
        {
            var userTeams = await GetMembershipOfUser(ct, userId);
            if (!userTeams.Any(x => x.TeamId == teamId))
            {
                return Result.Fail(new TeamUserNotATeamParticipantError(userId));
            }

            return Result.Ok();
        }

        public async Task<TeamParticipant[]> FindTeamParticipantByUsername(CancellationToken ct, string teamId, string? query, int take, int skip)
        {
            var teamParticipants = await GetParticipantsFromTeam(ct, teamId);
            var participantDict = teamParticipants.ToDictionary(x => x.UserId);

            var filter = new TeamUserFilterParams(query, take, skip, participantDict.Keys.ToArray());
            var users = await _userService.FindUsers(ct, filter);

            var result = new List<TeamParticipant>();
            users.ToList().ForEach(x =>
            {
                var user = participantDict[x.UserId];
                result.Add(user);
            });

            return result.ToArray();
        }
    }
}