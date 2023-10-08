using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake;
using Garnet.Teams.Infrastructure.Api.TeamJoinProjectRequest;
using Garnet.Teams.Infrastructure.MongoDb;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinProjectRequest
{
    [Binding]
    public class TeamJoinProjectRequestSteps : BaseSteps
    {
        private readonly ProjectTeamJoinRequestFakeConsumer _projectFake;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _queryExceptionsContext;

        public TeamJoinProjectRequestSteps(
            ProjectTeamJoinRequestFakeConsumer projectFake,
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            StepsArgs args) : base(args)
        {
            _projectFake = projectFake;
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [Given(@"существует проект '(.*)'")]
        public Task GivenСуществуетПроект(string projectId)
        {
            _projectFake.CreateProject(projectId);
            return Task.CompletedTask;
        }

        [When(@"пользователь '(.*)' отправляет заявку на вступление в проект '(.*)' от лица команды '(.*)'")]
        public async Task WhenПользовательОтправляетЗаявкуНаВступлениеВПроектОтЛицаКоманды(string username, string projectId, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            var claims = _currentUserProviderFake.LoginAs(username);
            var input = new TeamJoinProjectRequestPayload(Uuid.NewMongo(), team.Id, projectId);

            try
            {
                await Mutation.TeamJoinProjectRequest(CancellationToken.None, claims, input);

            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"в проекте '(.*)' существует заявка на вступление от команды '(.*)'")]
        public async Task ThenВПроектеСуществуетЗаявкаНаВступлениеОтКоманды(string projectId, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var projectTeams = _projectFake.GetProjectTeams(projectId);
            projectTeams.Any(x => x == team.Id).Should().Be(true);
        }

        [Given(@"существует заявка на вступление в проект '(.*)' от лица команды '(.*)'")]
        public async Task GivenСуществуетЗаявкаНаВступлениеВПроектОтЛицаКоманды(string projectId, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var joinProjectRequest = TeamJoinProjectRequestDocument.Create(Uuid.NewMongo(), team.Id, projectId);

            await Db.TeamJoinProjectRequests.InsertOneAsync(joinProjectRequest);
            _projectFake.AddTeamToProject(team.Id, projectId);
        }
    }
}