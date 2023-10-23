using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;

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
        public Task WhenОтменяетЗаявкуНаВступлениеВ(string вася, string fooBar)
        {
            return Task.CompletedTask;
        }
    }
}