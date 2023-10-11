using FluentAssertions;
using Garnet.Teams.Infrastructure.Api.TeamsFilter;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.Team;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamsFilter
{
    [Binding]
    public class TeamsFilterSteps : BaseSteps
    {
        private readonly FilterDefinitionBuilder<TeamDocument> _f = Builders<TeamDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamDocument> _u = Builders<TeamDocument>.Update;
        private TeamsFilterPayload _result = null!;

        public TeamsFilterSteps(StepsArgs args) : base(args)
        {
        }

        [Given(@"описание команды '([^']*)' состоит из '([^']*)'")]
        public async Task GivenОписаниеКомандыСостоитИз(string teamName, string description)
        {
            await Db.Teams.FindOneAndUpdateAsync(
                _f.Eq(x => x.Name, teamName),
                _u.Set(o => o.Description, description)
            );
        }

        [Given(@"теги команды '([^']*)' состоят из '([^']*)'")]
        public async Task GivenТегиКомандыСостоятИз(string teamName, string tags)
        {
            var teamTags = tags.Split(',').Select(x=> x.Trim());
            await Db.Teams.FindOneAndUpdateAsync(
                _f.Eq(x => x.Name, teamName),
                _u.Set(o => o.Tags, teamTags)
            );
        }

        [When(@"производится поиск команд по запросу '([^']*)'")]
        public async Task WhenПроизводитсяПоискКомандПоЗапросу(string query)
        {
            _result = await Query.TeamsFilter(CancellationToken.None, new TeamsFilterInput(query, null, 0, 100));
        }

        [When(@"производится поиск команд по тегам '([^']*)'")]
        public async Task WhenПроизводитсяПоискКомандПоТегу(string tags)
        {
            var teamTags = tags.Split(',');
            _result = await Query.TeamsFilter(CancellationToken.None, new TeamsFilterInput(null, teamTags, 0, 100));
        }

        [Then(@"в списке отображается '(.*)' команда")]
        public Task ThenВСпискеОтображаетсяКоманда(int resultCount)
        {
            _result.Teams.Count().Should().Be(resultCount);
            return Task.CompletedTask;
        }
    }
}