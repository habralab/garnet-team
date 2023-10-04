namespace Garnet.Teams.Application
{
    public record TeamUserFilterParams(
        string? Search,
        int Take, 
        int Skip,
        string[]? UserIds  
    );
}