using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamEditName
{
    [Binding]
    public class TeamEditNameSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _queryExceptionsContext;

        public TeamEditNameSteps(
            QueryExceptionsContext queryExceptionsContext,
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [When(@"'(.*)' редактирует название команды '(.*)' на '(.*)'")]
        public Task WhenРедактируетНазваниеКоманды(string username, string teamName, string newName)
        {
            return Task.CompletedTask;
        }

        [Then(@"в списке команд пользователя '(.*)' есть команда '(.*)'")]
        public async Task ThenВСпискеКомандПользователяЕстьКоманда(string username, string teamName)
        {
            var userTeams = await Db.Teams.Find(x => 
                x.OwnerUserId == _currentUserProviderFake.GetUserIdByUsername(username)
            ).ToListAsync();

            userTeams.Any(x=> x.Name == teamName).Should().BeTrue();
        }
    }
}