namespace Garnet.Teams.Infrastructure.Api.TeamJoinInvitationDecide
{
    public record TeamJoinInvitationDecideInput(
        string JoinInvitationId,
        bool IsApproved
    );
}