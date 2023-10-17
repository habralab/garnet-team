using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditUsername;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserEditUsername
{
    [Binding]
    public class UserEditUsernameSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        public UserEditUsernameSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"пользователь '(.*)' меняет в своем профиле ник на '(.*)'")]
        public async Task WhenПользовательМеняетВСвоемПрофилеНикНа(string username, string newUsername)
        {
            _currentUserProviderFake.LoginAs(username);
            var input = new UserEditUsernameInput(newUsername);
            await Mutation.UserEditUsername(CancellationToken.None, input);
        }

        [Then(@"в системе есть пользователь с ником '(.*)'")]
        public async Task ThenВСистемеЕстьПользовательСНиком(string username)
        {
            var user = await Db.Users.Find(x => x.UserName == username).FirstOrDefaultAsync();
            user.Should().NotBeNull();
        }

    }
}