using Garnet.Projects.Infrastructure.EventHandlers.ProjectTeamJoinRequest;
using Garnet.Teams.Events.TeamJoinProjectRequest;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamLeaveProject
{
    [Binding]
    public class ProjectTeamLeaveProjectSteps : BaseSteps
    {
        private readonly ProjectTeamLeaveProjectConsumer _projectTeamLeaveProjectConsumer;
        public ProjectTeamLeaveProjectSteps(ProjectTeamLeaveProjectConsumer projectTeamLeaveProjectConsumer, StepsArgs args) : base(args)
        {
            _projectTeamLeaveProjectConsumer = projectTeamLeaveProjectConsumer;
        }

        [When(@"команда '(.*)' удаляется из состава проекта '(.*)'")]
        public async Task WhenКомандаУдаляетсяИзСоставаПроекта(string teamName, string projectName)
        {
            var team = await Db.ProjectTeams.Find(x=> x.TeamName == teamName).FirstAsync();
            var project = await Db.Projects.Find(x=> x.ProjectName == projectName).FirstAsync();

            var @event=new TeamLeaveProjectEvent(team.Id, project.Id);
            await _projectTeamLeaveProjectConsumer.Consume(@event);
        }
    }
}