namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestShowCreatedCheck
{
    [Binding]
    public class TeamUserJoinRequestShowCreatedCheckSteps : BaseSteps
    {
        public TeamUserJoinRequestShowCreatedCheckSteps(StepsArgs args) : base(args)
        {
        }

        [Given(@"заявка на вступление в команду '(.*)' от пользователя '(.*)' была создана '(.*)'")]
        public Task GivenЗаявкаНаВступлениеВКомандуОтПользователяБылаСоздана(string teamName, string username, string date)
        {
            return Task.CompletedTask;
        }

        [When(@"'(.*)' просматривает заявки на вступление в команду '(.*)' в порядке актуальности")]
        public Task WhenПросматриваетЗаявкиНаВступлениеВКомандуВПорядкеАктуальности(string username, string teamName)
        {
            return Task.CompletedTask;
        }

        [Then(@"дата создания первой заявки в списке равна '(.*)'")]
        public Task ThenДатаСозданияПервойЗаявкиВСпискеРавна(string date)
        {
            return Task.CompletedTask;
        }
    }
}