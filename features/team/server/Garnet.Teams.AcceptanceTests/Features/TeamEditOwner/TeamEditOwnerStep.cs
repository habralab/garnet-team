using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamEditOwner;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamEditOwner
{
    [Binding]
    public class TeamEditOwnerStep : BaseSteps
    {
        private QueryExceptionsContext _queryExceptionsContext;
        private CurrentUserProviderFake _currentUserProviderFake;
        public TeamEditOwnerStep(QueryExceptionsContext queryExceptionsContext, CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [When(@"'([^']*)' изменяет владельца команды '([^']*)' на пользователя '([^']*)'")]
        public async Task WhenИзменяетВладельцаКомандыНаПользователя(string username, string teamName, string newOwnerUsername)
        {
            var usernameClaims = _currentUserProviderFake.LoginAs(username);
            _currentUserProviderFake.LoginAs(newOwnerUsername);

            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var input = new TeamEditOwnerInput(team.Id, _currentUserProviderFake.UserId);

            try
            {
                await Mutation.TeamEditOwner(CancellationToken.None, usernameClaims, input);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }
    }
}