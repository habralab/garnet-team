using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Notifications.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace Garnet.Notifications.AcceptanceTests.Features.NotificationDelete
{
    [Binding]
    public class NotificationDeleteSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly UpdateDefinitionBuilder<NotificationDocument> _u = Builders<NotificationDocument>.Update;

        public NotificationDeleteSteps(
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"уведомление для пользователя '(.*)' имеет тип '(.*)' и ссылкой на '(.*)'")]
        public async Task GivenНазваниеУведомления(string username, string notificationType, string notificationLinkedEntityId)
        {
            await Db.Notifications.UpdateOneAsync(
                x => x.UserId == _currentUserProviderFake.GetUserIdByUsername(username),
                _u
                    .Set(x => x.Type, notificationType)
                    .Set(x => x.LinkedEntityId, notificationLinkedEntityId)
            );
        }

        [When(@"пользователь '(.*)' отмечает уведомление с типом '(.*)' и ссылкой на '(.*)' как прочитанное")]
        public async Task WhenПользовательОтмечаетУведомлениеКакПрочитанное(string username, string notificationType, string notificationLinkedEntityId)
        {
            var notification = await Db.Notifications
               .Find(x =>
                   x.UserId == _currentUserProviderFake.GetUserIdByUsername(username)
                   & x.Type == notificationType
                   & x.LinkedEntityId == notificationLinkedEntityId
                )
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

        [Then(@"у пользователя '(.*)' нет уведомления с типом '(.*)' и ссылкой на '(.*)'")]
        public async Task ThenУПользователяНетУведомленияСНазванием(string username, string notificationType, string notificationLinkedEntityId)
        {
            var notification = await Db.Notifications
                .Find(x =>
                    x.UserId == _currentUserProviderFake.GetUserIdByUsername(username)
                    & x.Type == notificationType
                    & x.LinkedEntityId == notificationLinkedEntityId
                )
                .FirstOrDefaultAsync();
            notification.Should().BeNull();
        }
    }
}