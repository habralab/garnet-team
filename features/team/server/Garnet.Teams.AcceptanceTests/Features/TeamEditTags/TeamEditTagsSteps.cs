using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamEditTags;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamEditTags
{
    [Binding]
    public class TeamEditTagsSteps : BaseSteps
    {
        private readonly QueryExceptionsContext _queryExceptionsContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public TeamEditTagsSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [When(@"'(.*)' редактирует теги команды '(.*)' на '(.*)'")]
        public async Task WhenРедактируетТегиКомандыНа(string username, string teamName, string tags)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var teamTags = tags.Split(',', StringSplitOptions.TrimEntries);

            var claims = _currentUserProviderFake.LoginAs(username);
            var input = new TeamEditTagsInput(team.Id, teamTags);

            try
            {
                await Mutation.TeamEditTags(CancellationToken.None, claims, input);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
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