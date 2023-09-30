namespace Garnet.Teams.Infrastructure.Api.TeamGet
{
    public record TeamGetPayload(
        string Id,
        string Name,
        string Description
    );
}