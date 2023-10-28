using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Application.MessageBus;
using Garnet.Common.Infrastructure.Support;
using Garnet.Notifications.AcceptanceTests.Support;
using Garnet.Notifications.Infrastructure.Api.NotificationGet;
using MongoDB.Driver;

namespace Garnet.Notifications.AcceptanceTests.Features.NotificationGet
{
    [Binding]
    public class NotificationsGetListByCurrentUserSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly IMessageBus _messageBus;
        private NotificationGetPayload _result = null!;

        public NotificationsGetListByCurrentUserSteps(
            IMessageBus messageBus,
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _messageBus = messageBus;
        }

        [Given(@"существует пользователь '(.*)'")]
        public Task GivenСуществуетПользователь(string username)
        {
            _currentUserProviderFake.RegisterUser(username, Uuid.NewGuid());
            return Task.CompletedTask;
        }

        [Given(@"существует уведомление для пользователя '(.*)'")]
        public async Task GivenСуществуетУведомлениеДляПользователя(string username)
        {
            var notification = GiveMe.Notification()
                .WithUserId(_currentUserProviderFake.GetUserIdByUsername(username));
            var @event = GiveMe.EventFromNotification(notification);

            await _messageBus.Publish(@event);
        }

        [When(@"пользователь '(.*)' проверяет уведомления")]
        public async Task WhenПользовательПроверяетУведомления(string username)
        {
            _currentUserProviderFake.LoginAs(username);
            _result = await Query.NotificationsGetListByCurrentUser(CancellationToken.None);
        }

        [Then(@"количество полученных пользователем '(.*)' уведомлений равно '(.*)'")]
        public Task ThenКоличествоПолученныхПользователемУведомленийРавно(string username, int notificationCount)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            _result.Notifications.Length.Should().Be(notificationCount);
            _result.Notifications.All(x => x.UserId == userId).Should().BeTrue();
            return Task.CompletedTask;
        }
    }
}