namespace Garnet.Teams.Application.TeamUser.Args
{
    public record TeamUserFilterArgs(
        string? Search,
        int Take, 
        int Skip
    );
}