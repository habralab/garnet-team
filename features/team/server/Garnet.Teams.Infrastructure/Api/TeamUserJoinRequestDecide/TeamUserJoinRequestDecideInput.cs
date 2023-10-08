namespace Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestApprove
{
    public record TeamUserJoinRequestDecideInput(
        string UserJoinRequestId,
        bool IsApproved
    );
}