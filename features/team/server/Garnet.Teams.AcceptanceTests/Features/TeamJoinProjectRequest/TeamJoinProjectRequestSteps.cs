namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinProjectRequest
{
    [Binding]
    public class TeamJoinProjectRequestSteps : BaseSteps
    {
        public TeamJoinProjectRequestSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"пользователь '(.*)' отправляет заявку на вступление в проект '(.*)' от лица команды '(.*)'")]
        public Task WhenПользовательОтправляетЗаявкуНаВступлениеВПроектОтЛицаКоманды(string username, string projectName, string teamName)
        {
            return Task.CompletedTask;
        }
    }
}