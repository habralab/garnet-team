using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using MongoDB.Driver;

namespace Garnet.Notifications.AcceptanceTests.Features.NotificationDelete
{
    [Binding]
    public class NotificationDeleteSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public NotificationDeleteSteps(
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"пользователь '(.*)' отмечает уведомление как прочитанное")]
        public async Task WhenПользовательОтмечаетУведомлениеКакПрочитанное(string username)
        {
            var notification = await Db.Notifications
                .Find(x => x.UserId == _currentUserProviderFake.GetUserIdByUsername(username))
                .FirstAsync();

            _currentUserProviderFake.LoginAs(username);
            await Mutation.NotificationDelete(CancellationToken.None, notification.Id);
        }

        [Then(@"в системе количество уведомлений пользователя '(.*)' равно '(.*)'")]
        public async Task ThenВСистемеКоличествоУведомленийПользователяРавно(string username, int notificationCount)
        {
            var notifications = await Db.Notifications
                .Find(x => x.UserId == _currentUserProviderFake.GetUserIdByUsername(username))
                .ToListAsync();
            notifications.Count.Should().Be(notificationCount);
        }
    }
}