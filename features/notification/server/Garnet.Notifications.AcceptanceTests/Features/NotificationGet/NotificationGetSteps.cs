using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Application.MessageBus;
using Garnet.Common.Infrastructure.Support;
using Garnet.Notifications.AcceptanceTests.Support;
using MongoDB.Driver;

namespace Garnet.Notifications.AcceptanceTests.Features.NotificationGet
{
    [Binding]
    public class NotificationGetSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly IMessageBus _messageBus;
        public NotificationGetSteps(
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

            await Db.Notifications.InsertOneAsync(notification);
        }

        [When(@"появляется уведомление для пользователя '(.*)'")]
        public async Task WhenПоявляетсяУведомлениеДляПользователя(string username)
        {
            var notification = GiveMe.Notification()
                .WithUserId(_currentUserProviderFake.GetUserIdByUsername(username));
            var @event = GiveMe.EventFromNotification(notification);

            await _messageBus.Publish(@event);
        }

        [Then(@"количество полученных пользователем '(.*)' уведомлений равно '(.*)'")]
        public Task ThenКоличествоПолученныхПользователемУведомленийРавно(string username, int notificationCount)
        {
            return Task.CompletedTask;
        }
    }
}