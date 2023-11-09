using MongoDB.Driver;

namespace Garnet.NewsFeed.Infrastructure.MongoDB
{
    public class Db
    {
        private readonly IMongoDatabase _mongoDatabase;

        public Db(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<NewsFeedPostDocument> NewsFeedPost => _mongoDatabase.GetCollection<NewsFeedPostDocument>("NewsFeedPost");
    }
}