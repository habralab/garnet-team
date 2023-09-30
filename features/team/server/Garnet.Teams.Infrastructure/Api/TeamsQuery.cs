using System.Security.Claims;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api.TeamGet;
using HotChocolate.Execution;
using HotChocolate.Types;

namespace Garnet.Teams.Infrastructure.Api
{
    [ExtendObjectType("Query")]

    public class TeamsQuery
    {
        private readonly TeamService _teamService;

        public TeamsQuery(TeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<TeamPayload> TeamGet(CancellationToken ct, string teamId)
        {
            var team = await _teamService.GetTeamById(ct, teamId)
                ?? throw new QueryException($"Команда с идентификатором '{teamId}' не найдена");
            return new TeamPayload(team.Id, team.Name, team.Description, team.Tags);
        }
    }
}