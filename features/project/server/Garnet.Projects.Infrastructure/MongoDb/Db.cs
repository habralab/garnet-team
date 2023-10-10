using Garnet.Projects.Infrastructure.MongoDb.Project;
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
    public IMongoCollection<ProjectUserDocument> ProjectUsers => _mongoDatabase.GetCollection<ProjectUserDocument>("ProjectUsers");
    public IMongoCollection<ProjectTeamDocument> ProjectTeams => _mongoDatabase.GetCollection<ProjectTeamDocument>("ProjectTeams");
    public IMongoCollection<ProjectTeamParticipantDocument> ProjectTeamsParticipants => _mongoDatabase.GetCollection<ProjectTeamParticipantDocument>("ProjectTeamsParticipants");
    public IMongoCollection<ProjectTeamJoinRequestDocument> ProjectTeamJoinRequests => _mongoDatabase.GetCollection<ProjectTeamJoinRequestDocument>("ProjectTeamJoinRequests");
}