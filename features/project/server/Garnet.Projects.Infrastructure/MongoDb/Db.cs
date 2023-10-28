using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeam;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;
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
    public IMongoCollection<ProjectTaskDocument> ProjectTasks => _mongoDatabase.GetCollection<ProjectTaskDocument>("ProjectTasks");
}