using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestsShow;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestsShow
{
    [Binding]
    public class TeamUserJoinRequestsShowSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private TeamUserJoinRequestsShowPayload _result = null!;

        public TeamUserJoinRequestsShowSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"'(.*)' просматривает заявки на вступление в команду '(.*)'")]
        public async Task WhenПросматриваетЗаявкиНаВступлениеВКоманду(string username, string teamName)
        {
            _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            _result = await Query.TeamUserJoinRequestsShow(CancellationToken.None, team.Id);
        }

        [Then(@"количество заявок в списке равно '(.*)'")]
        public Task ThenВСпискеОтображаетсяКоманда(int resultCount)
        {
            _result.TeamUserJoinRequests.Count().Should().Be(resultCount);
            return Task.CompletedTask;
        }
    }
}