using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.FakeServices.NotificationFake;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvitationCancel
{
    [Binding]
    public class TeamJoinInvitationCancelSteps : BaseSteps
    {
        private readonly DeleteNotificationCommandMessageFakeConsumer _deleteNotificationCommandMessageFakeConsumer;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _queryExceptionsContext;

        public TeamJoinInvitationCancelSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            DeleteNotificationCommandMessageFakeConsumer deleteNotificationCommandMessageFakeConsumer,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
            _deleteNotificationCommandMessageFakeConsumer = deleteNotificationCommandMessageFakeConsumer;
        }

        [When(@"'(.*)' отменяет приглашение пользователя '(.*)' на вступление в команду '(.*)'")]
        public async Task WhenОтменяетПриглашениеПользователяНаВступлениеВКоманду(string ownerUsername, string username, string teamName)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var invitation = await Db.TeamJoinInvitations.Find(x => x.TeamId == team.Id & x.UserId == userId).FirstAsync();

            _currentUserProviderFake.LoginAs(ownerUsername);
            try
            {
                await Mutation.TeamJoinInvitationCancel(CancellationToken.None, invitation.Id);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"в команде '(.*)' количество приглашений на вступление равно '(.*)'")]
        public async Task ThenВКомандеКоличествоПриглашенийНаВступлениеРавно(string teamName, int invitationCount)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var invitations = await Db.TeamJoinInvitations.Find(x => x.TeamId == team.Id).ToListAsync();

            invitations.Count.Should().Be(invitationCount);
        }

        [Then(@"для пользователя '(.*)' нет уведомлений типа '(.*)'")]
        public async Task ThenДляПользователяНетУведомленийТипа(string username, string evenType)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var message = _deleteNotificationCommandMessageFakeConsumer.Notifications
               .First(x => x.UserId == user.Id);
            message.Type.Should().Be(evenType);
        }

        [Then(@"для пользователя '(.*)' нет уведомлений со связанной сущностью командой '(.*)'")]
        public async Task ThenДляПользователяНетУведомленийСоСвязаннойСущностьюКомандой(string username, string teamName)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var message = _deleteNotificationCommandMessageFakeConsumer.Notifications
               .First(x => x.UserId == user.Id);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            message.LinkedEntityId.Should().Be(team.Id);
        }
    }
}