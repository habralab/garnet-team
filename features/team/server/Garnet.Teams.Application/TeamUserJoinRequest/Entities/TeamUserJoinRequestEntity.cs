namespace Garnet.Teams.Application
{
    public record TeamUserJoinRequestEntity(
        string Id,
        string UserId,
        string TeamId
    );
}