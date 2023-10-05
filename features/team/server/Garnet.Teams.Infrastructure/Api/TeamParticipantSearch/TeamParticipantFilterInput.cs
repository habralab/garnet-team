namespace Garnet.Teams.Infrastructure.Api.TeamParticipantSearch
{
    public record TeamParticipantFilterInput(
        string TeamId,
        string? Search,
        int Skip,
        int Take
    );
}