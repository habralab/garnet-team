using Garnet.Notifications.Application;
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