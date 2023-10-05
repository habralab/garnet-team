namespace Garnet.Teams.Application
{
    public record TeamUserFilterArgs(
        string? Search,
        int Take, 
        int Skip
    );
}