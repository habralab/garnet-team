using Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedPost;
using Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeam;
using Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeamParticipant;
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
        public IMongoCollection<NewsFeedTeamDocument> NewsFeedTeam => _mongoDatabase.GetCollection<NewsFeedTeamDocument>("NewsFeedTeam");
        public IMongoCollection<NewsFeedTeamParticipantDocument> NewsFeedTeamParticipant => _mongoDatabase.GetCollection<NewsFeedTeamParticipantDocument>("NewsFeedTeamParticipant");
    }
}