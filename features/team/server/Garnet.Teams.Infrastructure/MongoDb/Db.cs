using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class Db
    {
        private readonly IMongoDatabase _mongoDatabase;

        public Db(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<TeamDocument> Teams => _mongoDatabase.GetCollection<TeamDocument>("Teams");
        public IMongoCollection<TeamParticipantDocument> TeamParticipants => _mongoDatabase.GetCollection<TeamParticipantDocument>("TeamParticipants");
    }
}