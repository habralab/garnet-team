using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.MongoDb;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinRequest
{
    [Binding]
    public class TeamUserJoinRequestStep : BaseSteps
    {
        private CurrentUserProviderFake _currentUserProviderFake;
        private QueryExceptionsContext _errorStepContext;

        public TeamUserJoinRequestStep(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext errorStepContext,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _errorStepContext = errorStepContext;
        }

        [Given(@"существует заявка на вступление в команду '(.*)' от пользователя '(.*)'")]
        public async Task GivenСуществуетЗаявкаНаВступлениеВКомандуОтПользователя(string teamName, string username)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var request = TeamUserJoinRequestDocument.Create(Uuid.NewMongo(), userId, team.Id);

            await Db.TeamUserJoinRequest.InsertOneAsync(request);
        }

        [When(@"пользователь '(.*)' подает заявку на вступление в команду '(.*)'")]
        public async Task WhenПользовательПодаетЗаявкуНаВступлениеВКоманду(string username, string teamName)
        {
            var claims = _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            try
            {
                await Mutation.TeamUserJoinRequestCreate(CancellationToken.None, claims, team.Id);
            }
            catch (QueryException ex)
            {
                _errorStepContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"в команде '(.*)' количество заявок на вступление равно '(.*)'")]
        public async Task ThenВКомандеКоличествоЗаявокНаВступлениеРавно(string teamName, int joinRequestCount)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var requestCount = await Db.TeamUserJoinRequest.Find(x => x.TeamId == team.Id).ToListAsync();
            requestCount.Count.Should().Be(joinRequestCount);
        }
    }
}