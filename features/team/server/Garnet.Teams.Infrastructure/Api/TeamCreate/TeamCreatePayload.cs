namespace Garnet.Teams.Infrastructure.Api.TeamCreate
{
    public record TeamCreatePayload(string Id, string OwnerUserId, string Name, string Description);
}