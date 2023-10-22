using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.MongoDb.TeamUserJoinRequest;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequest
{
    [Binding]
    public class TeamUserJoinRequestStep : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _errorStepContext;
        private readonly DateTimeServiceFake _dateTimeServiceFake;
        public TeamUserJoinRequestStep(
            DateTimeServiceFake dateTimeServiceFake,
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext errorStepContext,
            StepsArgs args) : base(args)
        {
            _dateTimeServiceFake = dateTimeServiceFake;
            _currentUserProviderFake = currentUserProviderFake;
            _errorStepContext = errorStepContext;
        }

        [Given(@"существует заявка на вступление в команду '(.*)' от пользователя '(.*)'")]
        public async Task GivenСуществуетЗаявкаНаВступлениеВКомандуОтПользователя(string teamName, string username)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var audit = AuditInfoDocument.Create(_dateTimeServiceFake.UtcNow, _currentUserProviderFake.UserId);
            var request = TeamUserJoinRequestDocument.Create(Uuid.NewMongo(), userId, team.Id);

            request = request with { AuditInfo = audit };
            await Db.TeamUserJoinRequests.InsertOneAsync(request);
        }

        [When(@"пользователь '(.*)' подает заявку на вступление в команду '(.*)'")]
        public async Task WhenПользовательПодаетЗаявкуНаВступлениеВКоманду(string username, string teamName)
        {
            _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            try
            {
                await Mutation.TeamUserJoinRequestCreate(CancellationToken.None, team.Id);
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
            var requestCount = await Db.TeamUserJoinRequests.Find(x => x.TeamId == team.Id).ToListAsync();
            requestCount.Count.Should().Be(joinRequestCount);
        }
    }
}