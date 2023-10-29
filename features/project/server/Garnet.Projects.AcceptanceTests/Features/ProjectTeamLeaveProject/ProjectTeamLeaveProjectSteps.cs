using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamLeaveProject
{
    [Binding]
    public class ProjectTeamLeaveProjectSteps : BaseSteps
    {
        public ProjectTeamLeaveProjectSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"команда '(.*)' удаляется из состава проекта '(.*)'")]
        public Task WhenКомандаУдаляетсяИзСоставаПроекта(string teamName, string projectName)
        {
            return Task.CompletedTask;
        }
    }
}