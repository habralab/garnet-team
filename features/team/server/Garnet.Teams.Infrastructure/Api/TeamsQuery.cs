using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api.TeamGet;
using Garnet.Teams.Infrastructure.Api.TeamsFilter;
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
            var result = await _teamService.GetTeamById(ct, teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamPayload(team.Id, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamsFilterPayload> TeamsFilter(CancellationToken ct, TeamsFilterInput input)
        {
            var teams = await _teamService.FilterTeams(
                ct,
                input.Search,
                input.Tags ?? Array.Empty<string>(),
                input.Skip,
                input.Take);

            return new TeamsFilterPayload(teams.Select(x => new TeamPayload(x.Id, x.Name, x.Description, x.Tags)).ToArray());
        }
    }
}