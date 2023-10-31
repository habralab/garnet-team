using Garnet.Common.Infrastructure.Support;
using Garnet.Notifications.Application;
using Garnet.Notifications.Application.Args;
using MongoDB.Driver;

namespace Garnet.Notifications.Infrastructure.MongoDB
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<NotificationDocument> _f = Builders<NotificationDocument>.Filter;

        public NotificationRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task CreateNotification(CancellationToken ct, NotificationCreateArgs args)
        {
            var db = _dbFactory.Create();

            var notification = NotificationDocument.Create(Uuid.NewMongo(), args);
            await db.Notifications.InsertOneAsync(notification, cancellationToken: ct);
        }

        public async Task DeleteNotificationById(CancellationToken ct, string notificationId)
        {
            var db = _dbFactory.Create();
            await db.Notifications.DeleteOneAsync(
                _f.Eq(x => x.Id, notificationId),
                cancellationToken: ct
            );
        }

        public async Task<NotificationEntity?> GetNotificationById(CancellationToken ct, string notificationId)
        {
            var db = _dbFactory.Create();
            var notification = await db.Notifications.Find(
                _f.Eq(x => x.Id, notificationId)
            ).FirstOrDefaultAsync(ct);

            return notification is null ? null : NotificationDocument.ToDomain(notification);
        }

        public async Task<NotificationEntity[]> GetNotificationsByUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();
            var notifications = await db.Notifications.Find(
                _f.Eq(x => x.UserId, userId)
            ).ToListAsync(ct);

            return notifications.Select(x => NotificationDocument.ToDomain(x)).ToArray();
        }
    }
}