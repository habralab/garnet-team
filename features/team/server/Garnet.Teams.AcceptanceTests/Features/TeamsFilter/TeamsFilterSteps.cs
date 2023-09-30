using Garnet.Teams.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamsFilter
{
    [Binding]
    public class TeamsFilterSteps : BaseSteps
    {
        private readonly FilterDefinitionBuilder<TeamDocument> _f = Builders<TeamDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamDocument> _u = Builders<TeamDocument>.Update;

        public TeamsFilterSteps(StepsArgs args) : base(args)
        {
        }

        [Given(@"описание команды '([^']*)' состоит из '([^']*)'")]
        public async Task GivenОписаниеКомандыСостоитИз(string teamName, string description)
        {
            await Db.Teams.FindOneAndUpdateAsync(
                _f.Eq(x=> x.Name, teamName),
                _u.Set(o => o.Description, description),
                options: new FindOneAndUpdateOptions<TeamDocument>
                {
                    ReturnDocument = ReturnDocument.After
                },
                CancellationToken.None
            );
        }

        [Given(@"теги команды '([^']*)' состоят из '([^']*)'")]
        public Task GivenТегиКомандыСостоятИз(string teamName, string tags)
        {
            return Task.CompletedTask;
        }

        [When(@"производится поиск команд по запросу '([^']*)'")]
        public Task WhenПроизводитсяПоискКомандПоЗапросу(string query)
        {
            return Task.CompletedTask;
        }

        [Then(@"в списке отображается '(\d*)' команда")]
        public Task ThenВСпискеОтображаетсяКоманда(int resultCount)
        {
            return Task.CompletedTask;
        }

        [When(@"производится поиск команд по тегу '([^']*)'")]
        public Task WhenПроизводитсяПоискКомандПоТегу(string query)
        {
            return Task.CompletedTask;
        }
    }
}