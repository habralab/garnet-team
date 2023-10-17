using FluentAssertions;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserEditUsername
{
    [Binding]
    public class UserEditUsernameSteps : BaseSteps
    {
        public UserEditUsernameSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"пользователь '(.*)' меняет в своем профиле ник на '(.*)'")]
        public Task WhenПользовательМеняетВСвоемПрофилеНикНа(string username, string newUsername)
        {
            return Task.CompletedTask;
        }

        [Then(@"в системе есть пользователь с ником '(.*)'")]
        public async Task ThenВСистемеЕстьПользовательСНиком(string username)
        {
            var user = await Db.Users.Find(x => x.UserName == username).FirstOrDefaultAsync();
            user.Should().NotBeNull();
        }

    }
}