using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestsShow;
using Garnet.Teams.Infrastructure.MongoDb;
using MongoDB.Driver;
using TechTalk.SpecFlow.Assist;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestShow
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
            var claims = _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            _result = await Query.TeamUserJoinRequestsShow(CancellationToken.None, claims, team.Id);
        }

        [Then(@"в списке отображается '(.*)' заявка")]
        public Task ThenВСпискеОтображаетсяКоманда(int resultCount)
        {
            _result.TeamUserJoinRequests.Count().Should().Be(resultCount);
            return Task.CompletedTask;
        }
    }
}