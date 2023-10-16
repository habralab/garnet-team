namespace Garnet.Teams.Infrastructure.Api.TeamsList
{
    public record TeamsListInput(
        string UserId,
        int Skip,
        int Take
    );
}