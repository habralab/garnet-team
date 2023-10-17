using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamEditName;
using HotChocolate.Execution;
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
        public async Task WhenРедактируетНазваниеКоманды(string username, string teamName, string newName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            _currentUserProviderFake.LoginAs(username);
            var input = new TeamEditNameInput(team.Id, newName);

            try
            {
                await Mutation.TeamEditName(CancellationToken.None, input);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"в списке команд пользователя '(.*)' есть команда '(.*)'")]
        public async Task ThenВСпискеКомандПользователяЕстьКоманда(string username, string teamName)
        {
            var userTeams = await Db.Teams.Find(x =>
                x.OwnerUserId == _currentUserProviderFake.GetUserIdByUsername(username)
            ).ToListAsync();

            userTeams.Any(x => x.Name == teamName).Should().BeTrue();
        }
    }
}