using FluentAssertions;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamEditTags
{
    [Binding]
    public class TeamEditTagsSteps : BaseSteps
    {
        public TeamEditTagsSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"'(.*)' редактирует теги команды '(.*)' на '(.*)'")]
        public Task WhenРедактируетТегиКомандыНа(string username, string teamName, string tags)
        {
            return Task.CompletedTask;
        }

        [Then(@"теги команды '(.*)' состоят из '(.*)'")]
        public async Task ThenТегиКомандыСостоятИз(string teamName, string tags)
        {
            var expectedTags = tags.Split(',', StringSplitOptions.TrimEntries);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            team.Tags.Should().BeEquivalentTo(tags);
        }
    }
}