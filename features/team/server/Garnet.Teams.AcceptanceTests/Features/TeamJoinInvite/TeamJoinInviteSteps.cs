using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.FakeServices.NotificationFake;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.TeamJoinInvitation;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvite
{
    [Binding]
    public class TeamJoinInviteSteps : BaseSteps
    {
        private readonly QueryExceptionsContext _errorStepContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly DateTimeServiceFake _dateTimeServiceFake;
        private readonly SendNotificationCommandMessageFakeConsumer _sendNotificationCommandMessageFakeConsumer;

        public TeamJoinInviteSteps(
            DateTimeServiceFake dateTimeServiceFake,
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext errorStepContext,
            SendNotificationCommandMessageFakeConsumer sendNotificationCommandMessageFakeConsumer,
            StepsArgs args) : base(args)
        {
            _dateTimeServiceFake = dateTimeServiceFake;
            _currentUserProviderFake = currentUserProviderFake;
            _errorStepContext = errorStepContext;
            _sendNotificationCommandMessageFakeConsumer = sendNotificationCommandMessageFakeConsumer;
        }

        [When(@"пользователь '(.*)' приглашает '(.*)' в команду '(.*)'")]
        public async Task WhenПользовательПриглашаетВКоманду(string teamOwner, string username, string teamName)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            _currentUserProviderFake.LoginAs(teamOwner);
            var input = new TeamJoinInviteInput(userId, team.Id);

            try
            {
                await Mutation.TeamJoinInvite(CancellationToken.None, input);
            }
            catch (QueryException ex)
            {
                _errorStepContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"у пользователя '(.*)' количество приглашений в команды равно '(.*)'")]
        public async Task ThenУПользователяКоличествоПриглашенийВКомандыРавно(string username, int joinInviteCount)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var invitations = await Db.TeamJoinInvitations.Find(x => x.UserId == user.Id).ToListAsync();
            invitations.Count.Should().Be(joinInviteCount);
        }

        [Given(@"существует приглашение пользователя '(.*)' на вступление в команду '(.*)' от владельца")]
        public async Task GivenСущесвуетПриглашениеПользователяНаВступлениеВКомандуОтВладельца(string username, string teamName)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var audit = AuditInfoDocument.Create(_dateTimeServiceFake.UtcNow, _currentUserProviderFake.UserId);

            var invitation = TeamJoinInvitationDocument.Create(Uuid.NewMongo(), user.Id, team.Id);
            invitation = invitation with { AuditInfo = audit };
            await Db.TeamJoinInvitations.InsertOneAsync(invitation);
        }

        [Then(@"для пользователя '(.*)' существует уведомление типа '(.*)'")]
        public async Task ThenДляПользователяСуществуетУведомлениеТипа(string username, string eventType)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var message = _sendNotificationCommandMessageFakeConsumer.Notifications
            .First(x=> x.UserId == user.Id);
            message!.Type.Should().Be(eventType);
        }

        [Then(@"в последнем уведомлении для пользователя '(.*)' связанной сущностью является команда '(.*)'")]
        public async Task ThenВПоследнемУведомленииДляПользователяСвязаннойСущностьюЯвляетсяКоманда(string username, string teamName)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var team = await Db.Teams.Find(x=> x.Name == teamName).FirstAsync();
            var message = _sendNotificationCommandMessageFakeConsumer.Notifications
            .Last(x=> x.UserId == user.Id);
            message.LinkedEntityId.Should().Be(team.Id);
        }
    }
}