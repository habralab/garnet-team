namespace Garnet.Teams.AcceptanceTests.Features.TeamsList
{
    [Binding]
    public class TeamsListSteps : BaseSteps
    {
        public TeamsListSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"производится запрос списка команд пользователя '(.*)'")]
        public Task WhenПроизводитсяЗапросСпискаКомандПользователя(string вася)
        {
            return Task.CompletedTask;
        }

        [Then(@"количество результатов списка команд пользователя равно '(.*)'")]
        public Task ThenКоличествоРезультатовСпискаКомандПользователяРавно(string p0)
        {
            return Task.CompletedTask;
        }
    }
}