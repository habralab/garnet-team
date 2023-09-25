using MongoDB.Driver;

namespace Garnet.Users.Infrastructure.MongoDb;

public class DbFactory
{
    private const string DbName = "Users";
    private readonly IMongoClient _client;

    public DbFactory(string connectionString)
    {
        _client = new MongoClient(connectionString);
    }
    
    public Db Create()
    {
        var database = _client.GetDatabase(DbName);
        return new Db(database);
    }
}