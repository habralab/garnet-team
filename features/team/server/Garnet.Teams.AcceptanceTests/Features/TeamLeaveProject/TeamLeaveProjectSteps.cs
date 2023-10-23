namespace Garnet.Teams.AcceptanceTests.Features.TeamLeaveProject
{
    [Binding]
    public class TeamLeaveProjectSteps : BaseSteps
    {
        public TeamLeaveProjectSteps(StepsArgs args) : base(args)
        {
        }

        [Given(@"команда '(.*)' является участником проекта '(.*)'")]
        public Task GivenКомандаЯвляетсяУчастникомПроекта(string teamName, string project)
        {
            return Task.CompletedTask;
        }

        [When(@"'(.*)' удаляет команду '(.*)' из состава проекта '(.*)'")]
        public Task WhenУдаляетКомандуИзСоставаПроекта(string username, string teamName, string project)
        {
            return Task.CompletedTask;

        }

        [Then(@"команда '(.*)' не является участником проекта '(.*)'")]
        public Task ThenКомандаНеЯвляетсяУчастникомПроекта(string teamName, string project)
        {
            return Task.CompletedTask;

        }
    }
}