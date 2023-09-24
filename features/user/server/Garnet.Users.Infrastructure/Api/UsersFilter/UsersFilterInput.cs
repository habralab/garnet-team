namespace Garnet.Users.Infrastructure.Api.UsersFilter;

public record UsersFilterInput(
    string? Search,
    string[]? Tags,
    int Skip,
    int Take
);