using FluentAssertions;
using Garnet.Users.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserEditTags
{
    [Binding]
    public class UserEditTagsSteps : BaseSteps
    {
        private readonly FilterDefinitionBuilder<UserDocument> _f = Builders<UserDocument>.Filter;
        private readonly UpdateDefinitionBuilder<UserDocument> _u = Builders<UserDocument>.Update;

        public UserEditTagsSteps(StepsArgs args) : base(args)
        {
        }

        [Given(@"теги пользователя '(.*)' состоят из '(.*)'")]
        public async Task GivenТегиПользователяСостоятИз(string username, string tags)
        {
            var userTags = tags.Split(',', StringSplitOptions.RemoveEmptyEntries);

            await Db.Users.UpdateOneAsync(
                _f.Eq(x => x.UserName, username),
                _u.Set(x => x.Tags, userTags)
            );
        }

        [When(@"'(.*)' редактирует свои теги на '(.*)'")]
        public void WhenРедактируетСвоиТегиНа(string username, string tags)
        {
        }

        [Then(@"теги пользователя '(.*)' состоят из '(.*)'")]
        public async void ThenТегиПользователяСостоятИз(string username, string tags)
        {
            var user = await Db.Users.Find(x => x.UserName == username).FirstAsync();
            var userTags = tags.Split(',', StringSplitOptions.RemoveEmptyEntries);

            user.Tags.Should().BeEquivalentTo(userTags);
        }
    }
}