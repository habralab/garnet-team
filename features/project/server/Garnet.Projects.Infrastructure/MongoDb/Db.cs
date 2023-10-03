using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb;

public class Db
{
    private readonly IMongoDatabase _mongoDatabase;

    public Db(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    public IMongoCollection<ProjectDocument> Projects => _mongoDatabase.GetCollection<ProjectDocument>("Projects");
    public IMongoCollection<ProjectUserDocument> ProjectUsers => _mongoDatabase.GetCollection<ProjectUserDocument>("ProjectsUsers");
}