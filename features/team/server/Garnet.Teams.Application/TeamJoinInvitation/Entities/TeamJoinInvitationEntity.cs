namespace Garnet.Teams.Application
{
    public record TeamJoinInvitationEntity(
        string Id,
        string UserId,
        string TeamId
    );
}