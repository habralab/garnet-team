namespace Garnet.Teams.Infrastructure.Api.TeamCreate
{
    public record TeamCreateInput(string Name, string Description, string[] Tags, string AvatarUrl);
}