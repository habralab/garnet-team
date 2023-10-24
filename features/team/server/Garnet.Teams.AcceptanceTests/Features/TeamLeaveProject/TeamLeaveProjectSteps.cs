using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake;
using Garnet.Teams.Infrastructure.Api.TeamLeaveProject;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamLeaveProject
{
    [Binding]
    public class TeamLeaveProjectSteps : BaseSteps
    {
        private readonly ProjectTeamLeaveFakeConsumer _projectFake;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _queryExceptionsContext;

        public TeamLeaveProjectSteps(
            QueryExceptionsContext queryExceptionsContext,
            CurrentUserProviderFake currentUserProviderFake,
            ProjectTeamLeaveFakeConsumer projectFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _projectFake = projectFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [Given(@"команда '(.*)' является участником проекта '(.*)'")]
        public async Task GivenКомандаЯвляетсяУчастникомПроекта(string teamName, string project)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            _projectFake.AddTeamToProject(team.Id, project);
        }

        [When(@"'(.*)' удаляет команду '(.*)' из состава проекта '(.*)'")]
        public async Task WhenУдаляетКомандуИзСоставаПроекта(string username, string teamName, string project)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var input = new TeamLeaveProjectNotice(team.Id, project);

            _currentUserProviderFake.LoginAs(username);
            try
            {
                await Mutation.TeamLeaveProject(CancellationToken.None, input);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"команда '(.*)' не является участником проекта '(.*)'")]
        public async Task ThenКомандаНеЯвляетсяУчастникомПроекта(string teamName, string project)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            _projectFake.GetProjectTeams(project).Contains(team.Id).Should().BeFalse();
        }
    }
}