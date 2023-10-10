namespace Garnet.Projects.Application.Args;

public record ProjectFilterArgs(
    string? Search,
    string[] Tags,
    int Skip,
    int Take);