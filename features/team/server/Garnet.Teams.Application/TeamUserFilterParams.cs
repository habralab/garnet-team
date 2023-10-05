namespace Garnet.Teams.Application
{
    public record TeamParticipantFilterParams(
        string? Search,
        int Take, 
        int Skip
    );
}