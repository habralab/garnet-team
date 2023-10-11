namespace Garnet.Teams.Application.Team.Args
{
    public record TeamFilterArgs(
        string? Search,
        string[] Tags,
        int Skip,
        int Take
    );
}