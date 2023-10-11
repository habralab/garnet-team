namespace Garnet.Teams.Application.TeamParticipant.Args
{
    public record TeamParticipantFilterArgs(
        string? Search,
        string? TeamId,
        int Skip,
        int Take
    );
}