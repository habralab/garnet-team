using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestsShow;
using Garnet.Teams.Infrastructure.MongoDb.TeamUserJoinRequest;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestShowCreatedCheck
{
    [Binding]
    public class TeamUserJoinRequestShowCreatedCheckSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly UpdateDefinitionBuilder<TeamUserJoinRequestDocument> _u = Builders<TeamUserJoinRequestDocument>.Update;
        private readonly FilterDefinitionBuilder<TeamUserJoinRequestDocument> _f = Builders<TeamUserJoinRequestDocument>.Filter;
        private TeamUserJoinRequestsShowPayload _result = null!;

        public TeamUserJoinRequestShowCreatedCheckSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"заявка на вступление в команду '(.*)' от пользователя '(.*)' была создана '(.*)'")]
        public async Task GivenЗаявкаНаВступлениеВКомандуОтПользователяБылаСоздана(string teamName, string username, string date)
        {
            _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var created = DateTimeOffset.Parse(date);
            var audit = AuditInfoDocument.Create(created, _currentUserProviderFake.UserId);

            await Db.TeamUserJoinRequests.UpdateOneAsync(
                _f.And(_f.Eq(x => x.TeamId, team.Id), _f.Eq(x => x.UserId, _currentUserProviderFake.GetUserIdByUsername(username))),
                _u.Set(x => x.AuditInfo, audit)
            );
        }

        [When(@"'(.*)' просматривает заявки на вступление в команду '(.*)' в порядке актуальности")]
        public async Task WhenПросматриваетЗаявкиНаВступлениеВКомандуВПорядкеАктуальности(string username, string teamName)
        {
            _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            _result = await Query.TeamUserJoinRequestsShow(CancellationToken.None, team.Id);
        }

        [Then(@"дата создания первой заявки в списке равна '(.*)'")]
        public Task ThenДатаСозданияПервойЗаявкиВСпискеРавна(string date)
        {
            var created = DateTimeOffset.Parse(date);
            _result.TeamUserJoinRequests.First()
                .Should()
                .Match<TeamUserJoinRequestShowPayload>(x => x.CreatedAt == created);
            return Task.CompletedTask;
        }
    }
}