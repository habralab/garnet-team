using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinRequest
{
    [Binding]
    public class TeamUserJoinRequestStep : BaseSteps
    {
        private CurrentUserProviderFake _currentUserProviderFake;
        public TeamUserJoinRequestStep(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"пользователь '(.*)' подает заявку на вступление в команду '(.*)'")]
        public async Task WhenПользовательПодаетЗаявкуНаВступлениеВКоманду(string username, string teamName)
        {
            var claims = _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            await Mutation.TeamUserJoinRequestCreate(CancellationToken.None, claims, team.Id);
        }

        [Then(@"в команде '(.*)' количество заявок на вступление равно '(.*)'")]
        public async Task ThenВКомандеКоличествоЗаявокНаВступлениеРавно(string teamName, int joinRequestCount)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var requestCount = await Db.TeamUserJoinRequest.Find(x => x.TeamId == team.Id).ToListAsync();
            requestCount.Count.Should().Be(joinRequestCount);
        }
    }
}