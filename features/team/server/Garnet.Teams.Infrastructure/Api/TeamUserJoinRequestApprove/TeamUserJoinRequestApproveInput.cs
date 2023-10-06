namespace Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestApprove
{
    public record TeamUserJoinRequestApproveInput(
        string UserJoinRequestId,
        bool IsApproved
    );
}