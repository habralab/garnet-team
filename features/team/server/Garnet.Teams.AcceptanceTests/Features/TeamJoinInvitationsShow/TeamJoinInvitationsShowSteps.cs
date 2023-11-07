using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvitationsShow;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvitationsShow
{
    [Binding]
    public class TeamJoinInvitationsShowSteps : BaseSteps
    {
        private TeamJoinInvitationsShowPayload _result = null!;
        private readonly QueryExceptionsContext _queryExceptionsContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public TeamJoinInvitationsShowSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [When(@"'(.*)' просматривает список приглашений команды '(.*)'")]
        public async Task ThenПросматриваетСписокПриглашенийКоманды(string username, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            _currentUserProviderFake.LoginAs(username);

            try
            {
                _result = await Query.TeamJoinInvitationsShow(CancellationToken.None, team.Id);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"количество приглашений в списке равно '(.*)'")]
        public Task ThenКоличествоПриглашенийВСпискеРавно(int joinTeamCount)
        {
            _result.TeamJoinInvites.Count().Should().Be(joinTeamCount);
            return Task.CompletedTask;
        }
    }
}