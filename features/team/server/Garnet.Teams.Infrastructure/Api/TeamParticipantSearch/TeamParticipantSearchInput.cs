namespace Garnet.Teams.Infrastructure.Api.TeamParticipantSearch
{
    public record TeamParticipantSearchInput(
        string TeamId,
        string? Search,
        int Skip,
        int Take
    );
}