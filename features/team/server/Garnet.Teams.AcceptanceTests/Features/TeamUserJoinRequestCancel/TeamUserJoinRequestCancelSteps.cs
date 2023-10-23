using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestCancel
{
    [Binding]
    public class TeamUserJoinRequestCancelSteps : BaseSteps
    {
        private readonly QueryExceptionsContext _queryExceptionsContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public TeamUserJoinRequestCancelSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [When(@"'(.*)' отменяет заявку на вступление в '(.*)'")]
        public async Task WhenОтменяетЗаявкуНаВступлениеВ(string username, string teamName)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var userJoinRequest = await Db.TeamUserJoinRequests.Find(x =>
                x.TeamId == team.Id
                & x.UserId == userId
            ).FirstAsync();

            _currentUserProviderFake.LoginAs(username);

            try
            {
                await Mutation.TeamUserJoinRequestCancel(CancellationToken.None, userJoinRequest.Id);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }
    }
}