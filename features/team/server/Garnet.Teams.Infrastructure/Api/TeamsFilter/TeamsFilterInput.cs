namespace Garnet.Teams.Infrastructure.Api.TeamsFilter
{
    public record TeamsFilterInput(
            string? Search,
            string[]? Tags,
            int Skip,
            int Take
        );
}