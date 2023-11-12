using Garnet.Teams.Infrastructure.Api.TeamGet;
using Garnet.Teams.Infrastructure.Api.TeamsListByUser;

namespace Garnet.Teams.Infrastructure.Api.TeamsList
{
    public record TeamsListPayload(TeamByUserPayload[] Teams);
}