namespace Garnet.Projects.Application.Project.Args;

public record ProjectFilterArgs(
    string? Search,
    string[] Tags,
    int Skip,
    int Take);