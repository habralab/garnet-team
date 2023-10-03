using Garnet.Common.AcceptanceTests.Fakes;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinRequest
{
    [Binding]
    public class TeamJoinRequestStep : BaseSteps
    {
        private CurrentUserProviderFake _currentUserProviderFake;
        public TeamJoinRequestStep(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"пользователь '(.*)' подает заявку на вступление в команду '(.*)'")]
        public async Task WhenПользовательПодаетЗаявкуНаВступлениеВКоманду(string username, string teamName)
        {
            var claims = _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x=> x.Name == teamName).FirstAsync();
            await Mutation.TeamUserJoinRequest(CancellationToken.None, claims, team.Id);
        }

        [Then(@"в команде '(.*)' количество заявок на вступление равно '(.*)'")]
        public Task ThenВКомандеКоличествоЗаявокНаВступлениеРавно(string teamName, int joinRequestCount)
        {
            return Task.CompletedTask;
        }
    }
}