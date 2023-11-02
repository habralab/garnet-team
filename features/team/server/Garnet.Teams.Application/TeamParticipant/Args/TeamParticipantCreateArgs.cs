namespace Garnet.Teams.Application.TeamParticipant.Args
{
     public record TeamParticipantCreateArgs(
        string UserId,
        string Username,
        string? AvatarUrl,
        string TeamId
    );
}