using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamLeaveProject
{
    [Binding]
    public class TeamLeaveProjectSteps : BaseSteps
    {
        private readonly ProjectTeamLeaveFakeConsumer _projectFake;
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public TeamLeaveProjectSteps(
            CurrentUserProviderFake currentUserProviderFake,
            ProjectTeamLeaveFakeConsumer projectFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _projectFake = projectFake;
        }

        [Given(@"команда '(.*)' является участником проекта '(.*)'")]
        public async Task GivenКомандаЯвляетсяУчастникомПроекта(string teamName, string project)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            _projectFake.AddTeamToProject(team.Id, project);
        }

        [When(@"'(.*)' удаляет команду '(.*)' из состава проекта '(.*)'")]
        public Task WhenУдаляетКомандуИзСоставаПроекта(string username, string teamName, string project)
        {
            return Task.CompletedTask;

        }

        [Then(@"команда '(.*)' не является участником проекта '(.*)'")]
        public async Task ThenКомандаНеЯвляетсяУчастникомПроекта(string teamName, string project)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            _projectFake.GetProjectTeams(project).Contains(team.Id).Should().BeFalse();
        }
    }
}