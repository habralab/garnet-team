namespace Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestDecide
{
    public record TeamUserJoinRequestDecideInput(
        string UserJoinRequestId,
        bool IsApproved
    );
}