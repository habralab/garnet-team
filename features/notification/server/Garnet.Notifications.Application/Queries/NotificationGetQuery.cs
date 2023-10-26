using Garnet.Common.Application;

namespace Garnet.Notifications.Application.Queries
{
    public class NotificationGetQuery
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly INotificationRepository _notificationRepository;
        public NotificationGetQuery(ICurrentUserProvider currentUserProvider, INotificationRepository notificationRepository)
        {
            _currentUserProvider = currentUserProvider;
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationEntity[]> Query(CancellationToken ct)
        {
            return await _notificationRepository.GetNotificationsByUser(ct, _currentUserProvider.UserId);
        }
    }
}