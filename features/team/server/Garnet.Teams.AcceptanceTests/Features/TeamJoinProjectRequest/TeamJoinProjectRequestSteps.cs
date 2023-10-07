using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake;
using Garnet.Teams.Infrastructure.Api.TeamJoinProjectRequest;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinProjectRequest
{
    [Binding]
    public class TeamJoinProjectRequestSteps : BaseSteps
    {
        private readonly ProjectFake _projectFake;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        public TeamJoinProjectRequestSteps(
            ProjectFake projectFake,
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _projectFake = projectFake;
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"существует проект '(.*)'")]
        public Task GivenСуществуетПроект(string projectName)
        {
            _projectFake.CreateProject(projectName);
            return Task.CompletedTask;
        }

        [When(@"пользователь '(.*)' отправляет заявку на вступление в проект '(.*)' от лица команды '(.*)'")]
        public async Task WhenПользовательОтправляетЗаявкуНаВступлениеВПроектОтЛицаКоманды(string username, string projectName, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            var claims = _currentUserProviderFake.LoginAs(username);
            var input = new TeamJoinProjectRequestPayload(team.Id, projectName);

            await Mutation.TeamJoinProjectRequest(CancellationToken.None, claims, input);
        }

        [Then(@"в проекте '(.*)' существует заявка на вступление от команды '(.*)'")]
        public async Task ThenВПроектеСуществуетЗаявкаНаВступлениеОтКоманды(string projectName, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var projectTeams = _projectFake.GetProjectTeams(projectName);
            projectTeams.Any(x => x == team.Id).Should().Be(true);
        }
    }
}