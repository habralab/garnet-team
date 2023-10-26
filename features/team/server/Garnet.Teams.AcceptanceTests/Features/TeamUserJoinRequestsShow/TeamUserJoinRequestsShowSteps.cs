using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestsShow;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestsShow
{
    [Binding]
    public class TeamUserJoinRequestsShowSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _queryExceptionsContext;
        private TeamUserJoinRequestsShowPayload _result = null!;

        public TeamUserJoinRequestsShowSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            StepsArgs args) : base(args)
        {
            _queryExceptionsContext = queryExceptionsContext;
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"'(.*)' просматривает заявки на вступление в команду '(.*)'")]
        public async Task WhenПросматриваетЗаявкиНаВступлениеВКоманду(string username, string teamName)
        {
            _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            try
            {
                _result = await Query.TeamUserJoinRequestsShow(CancellationToken.None, team.Id);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"количество заявок в списке равно '(.*)'")]
        public Task ThenВСпискеОтображаетсяКоманда(int resultCount)
        {
            _result.TeamUserJoinRequests.Count().Should().Be(resultCount);
            return Task.CompletedTask;
        }
    }
}