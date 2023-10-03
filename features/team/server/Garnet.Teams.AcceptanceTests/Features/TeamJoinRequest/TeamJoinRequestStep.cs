namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinRequest
{
    [Binding]
    public class TeamJoinRequestStep : BaseSteps
    {
        public TeamJoinRequestStep(StepsArgs args) : base(args)
        {
        }

        [When(@"пользователь '(.*)' подает заявку на вступление в команду '(.*)'")]
        public Task WhenПользовательПодаетЗаявкуНаВступлениеВКоманду(string username, string teamName)
        {
            return Task.CompletedTask;
        }

        [Then(@"в команде '(.*)' количество заявок на вступление равно '(.*)'")]
        public Task ThenВКомандеКоличествоЗаявокНаВступлениеРавно(string teamName, int joinRequestCount)
        {
            return Task.CompletedTask;
        }
    }
}