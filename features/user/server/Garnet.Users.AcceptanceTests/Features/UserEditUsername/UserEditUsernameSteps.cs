using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditUsername;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserEditUsername
{
    [Binding]
    public class UserEditUsernameSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _queryExceptionsContext;
        public UserEditUsernameSteps(
            QueryExceptionsContext queryExceptionsContext,
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [When(@"пользователь '(.*)' меняет в своем профиле ник на '(.*)'")]
        public async Task WhenПользовательМеняетВСвоемПрофилеНикНа(string username, string newUsername)
        {
            _currentUserProviderFake.LoginAs(username);
            var input = new UserEditUsernameInput(newUsername);

            try
            {
                await Mutation.UserEditUsername(CancellationToken.None, input);

            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"в системе есть пользователь с ником '(.*)'")]
        public async Task ThenВСистемеЕстьПользовательСНиком(string username)
        {
            var user = await Db.Users.Find(x => x.UserName == username).FirstOrDefaultAsync();
            user.Should().NotBeNull();
        }

    }
}