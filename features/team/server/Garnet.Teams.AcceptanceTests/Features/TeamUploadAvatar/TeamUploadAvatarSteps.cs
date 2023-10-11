using System.Text;
using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamUploadAvatar;
using Garnet.Teams.Infrastructure.MongoDb.Team;
using HotChocolate.Execution;
using HotChocolate.Types;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUploadAvatar
{
    [Binding]
    public class TeamUploadAvatarSteps : BaseSteps
    {
        private readonly QueryExceptionsContext _queryExceptionsContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly FilterDefinitionBuilder<TeamDocument> _f = Builders<TeamDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamDocument> _u = Builders<TeamDocument>.Update;

        public TeamUploadAvatarSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [Given(@"аватаркой команды '(.*)' является ссылка '(.*)'")]
        public async Task GivenАватаркойПроектаЯвляется(string teamName, string avatar)
        {
            await Db.Teams.UpdateOneAsync(
                _f.Eq(x => x.Name, teamName),
                _u.Set(x => x.AvatarUrl, avatar)
            );
        }

        [When(@"'(.*)' редактирует аватарку команды '(.*)' на '(.*)'")]
        public async Task WhenРедактируетАватаркуПроектаНа(string username, string teamName, string avatar)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            var claims = _currentUserProviderFake.LoginAs(username);
            var input = new TeamUploadAvatarInput(
                team.Id,
                new StreamFile(avatar,
                    () => new MemoryStream(Encoding.Default.GetBytes(avatar))
                )
            );

            try
            {
                await Mutation.TeamUploadAvatar(CancellationToken.None, claims, input);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"аватаркой команды '(.*)' является ссылка '(.*)'")]
        public async Task ThenАватаркойПроектаЯвляетсяСсылка(string teamName, string avatar)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            avatar = avatar.Replace("ID", team.Id);
            team.AvatarUrl.Should().Be(avatar);
        }

        [Then(@"в удаленном хранилище для команды '(.*)' есть файл '(.*)'")]
        public async Task ThenВУдаленномХранилищеДляКоманлыЕстьФайл(string teamName, string avatar)
        {
            await ThenАватаркойПроектаЯвляетсяСсылка(teamName, avatar);
        }
    }
}