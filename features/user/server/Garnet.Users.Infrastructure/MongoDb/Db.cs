using MongoDB.Driver;

namespace Garnet.Users.Infrastructure.MongoDb;

public class Db
{
    private readonly IMongoDatabase _mongoDatabase;

    public Db(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    public IMongoCollection<UserDocument> Users => _mongoDatabase.GetCollection<UserDocument>("Users");
}