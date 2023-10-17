namespace Garnet.Users.Application.Args
{
    public record UserFilterArgs(
        string? Search,
        string[] Tags,
        int Skip,
        int Take
    );
}