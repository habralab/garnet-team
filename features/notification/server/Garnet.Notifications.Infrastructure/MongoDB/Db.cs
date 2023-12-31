using MongoDB.Driver;

namespace Garnet.Notifications.Infrastructure.MongoDB
{
    public class Db
    {
        private readonly IMongoDatabase _mongoDatabase;

        public Db(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<NotificationDocument> Notifications => _mongoDatabase.GetCollection<NotificationDocument>("Notifications");
    }
}