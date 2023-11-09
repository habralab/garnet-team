using Garnet.Common.Infrastructure.MongoDb.Migrations;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.Migration
{
    public class CreateIndexesNewsFeedPostMigration : IRepeatableMigration
    {
        public Task Execute(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}