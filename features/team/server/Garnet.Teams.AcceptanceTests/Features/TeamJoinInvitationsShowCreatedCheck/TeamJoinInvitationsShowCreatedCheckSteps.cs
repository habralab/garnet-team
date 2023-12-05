using FluentAssertions;
using FluentAssertions.Extensions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvitationsShow;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;
using Garnet.Teams.Infrastructure.MongoDb.TeamJoinInvitation;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvitationsShowCreatedCheckSteps
{
    [Binding]
    public class TeamJoinInvitationsShowCreatedCheckSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private TeamJoinInvitationsShowPayload _result = null!;
        private readonly UpdateDefinitionBuilder<TeamJoinInvitationDocument> _u = Builders<TeamJoinInvitationDocument>.Update;
        private readonly FilterDefinitionBuilder<TeamJoinInvitationDocument> _f = Builders<TeamJoinInvitationDocument>.Filter;

        public TeamJoinInvitationsShowCreatedCheckSteps(
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"приглашение в команду '(.*)' пользователя '(.*)' было создано '(.*)'")]
        public async Task GivenПриглашениеВКомандуПользователяБылоСоздано(string teamName, string username, string date)
        {
            _currentUserProviderFake.LoginAs(username);
            var created = DateTimeOffset.Parse(date);

            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var audit = AuditInfoDocument.Create(created, _currentUserProviderFake.UserId);

            await Db.TeamJoinInvitations.UpdateOneAsync(
               _f.And(_f.Eq(x => x.TeamId, team.Id), _f.Eq(x => x.UserId, _currentUserProviderFake.UserId)),
               _u.Set(x => x.AuditInfo, audit)
           );
        }

        [When(@"'(.*)' просматривает список приглашений команды '(.*)' в порядке актуальности")]
        public async Task ThenПросматриваетСписокПриглашенийКоманды(string username, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            _currentUserProviderFake.LoginAs(username);
            _result = await Query.TeamJoinInvitationsShow(CancellationToken.None, team.Id);
        }

        [Then(@"дата создания первого приглашения в списке равна '(.*)'")]
        public Task ThenДатаСозданияПервогоПриглашенияВСпискеРавна(string date)
        {
            var created = DateTimeOffset.Parse(date);
            _result.TeamJoinInvites.First()
                .Should()
                .Match<TeamJoinInvitationShowPayload>(x => x.CreatedAt == created);
            return Task.CompletedTask;
        }
    }
}