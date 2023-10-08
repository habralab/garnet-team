namespace Garnet.Teams.Application
{
    public record TeamJoinInvitationFilterArgs(
        string? UserId,
        string? TeamId
    );
}