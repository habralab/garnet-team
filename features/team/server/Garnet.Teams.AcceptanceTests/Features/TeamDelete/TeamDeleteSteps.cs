namespace Garnet.Teams.AcceptanceTests.Features.TeamDelete
{
    [Binding]
    public class TeamDeleteSteps : BaseSteps
    {
        public TeamDeleteSteps(StepsArgs args) : base(args)
        {
        }

        [Given(@"владельцем команды '([^']*)' является '([^']*)'")]
        public Task GivenВладелецемКомандыЯвляется(string username, string team)
        {
            return Task.CompletedTask;
        }

        [When(@"'([^']*)' удаляет команду '([^']*)'")]
        public Task WhenУдаляетКоманду(string username, string team)
        {
            return Task.CompletedTask;
        }

        [Then(@"команды '([^']*)' в системе не существует")]
        public Task ThenКомандыВСистемеНеСуществует(string team)
        {
            return Task.CompletedTask;
        }

        [Then(@"пользователь получает ошибку '([^']*)'")]
        public Task ThenПользовательПолучаетОшибку(string errorMsg)
        {
            return Task.CompletedTask;
        }
    }
}