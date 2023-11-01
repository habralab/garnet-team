using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.FakeServices.NotificationFake;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestCancel
{
    [Binding]
    public class TeamUserJoinRequestCancelSteps : BaseSteps
    {
        private readonly QueryExceptionsContext _queryExceptionsContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly DeleteNotificationCommandMessageFakeConsumer _deleteNotificationCommandMessageFakeConsumer;

        public TeamUserJoinRequestCancelSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            DeleteNotificationCommandMessageFakeConsumer deleteNotificationCommandMessageFakeConsumer,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
            _deleteNotificationCommandMessageFakeConsumer = deleteNotificationCommandMessageFakeConsumer;
        }

        [When(@"'(.*)' отменяет заявку пользователя '(.*)' на вступление в '(.*)'")]
        public async Task WhenОтменяетЗаявкуНаВступлениеПользователяВ(string username, string requestedUser, string teamName)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(requestedUser);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var userJoinRequest = await Db.TeamUserJoinRequests.Find(x =>
                x.TeamId == team.Id
                & x.UserId == userId
            ).FirstAsync();

            _currentUserProviderFake.LoginAs(username);

            try
            {
                await Mutation.TeamUserJoinRequestCancel(CancellationToken.None, userJoinRequest.Id);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"для пользователя '(.*)' нет уведомлений со связанной сущностью пользователь '(.*)'")]
        public Task ThenДляПользователяНетУведомленийСоСвязаннойСущностьюПользователь(string username, string linkedUsername)
        {
            var message = _deleteNotificationCommandMessageFakeConsumer.DeletedNotifications
               .First(x => x.UserId == _currentUserProviderFake.GetUserIdByUsername(username));
            message.LinkedEntityId!.Should().Be(linkedUsername);
            return Task.CompletedTask;
        }
    }
}