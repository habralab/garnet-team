namespace Garnet.Teams.Application
{
    public record TeamUserJoinRequest(
        string Id,
        string UserId,
        string TeamId
    );
}