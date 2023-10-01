namespace Garnet.Teams.Infrastructure.Api.TeamEditOwner
{
    public record TeamEditOwnerInput(
        string TeamId,
        string NewOwnerUserId
    );
}