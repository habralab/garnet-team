namespace Garnet.Teams.Infrastructure.Api.TeamEditTags
{
    public record TeamEditTagsInput(
        string Id,
        string[] Tags
    );
}