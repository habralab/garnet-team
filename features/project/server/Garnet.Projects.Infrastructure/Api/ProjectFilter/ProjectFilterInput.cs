namespace Garnet.Projects.Infrastructure.Api.ProjectFilter;

public record ProjectFilterInput(
    string? Search,
    string[]? Tags,
    int Skip,
    int Take
);