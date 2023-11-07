using Garnet.Teams.Infrastructure.MongoDb.ProjectTeamParticipant;
using Garnet.Teams.Infrastructure.MongoDb.Team;
using Garnet.Teams.Infrastructure.MongoDb.TeamJoinInvitation;
using Garnet.Teams.Infrastructure.MongoDb.TeamJoinProjectRequest;
using Garnet.Teams.Infrastructure.MongoDb.TeamParticipant;
using Garnet.Teams.Infrastructure.MongoDb.TeamUser;
using Garnet.Teams.Infrastructure.MongoDb.TeamUserJoinRequest;
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
        public IMongoCollection<TeamUserDocument> TeamUsers => _mongoDatabase.GetCollection<TeamUserDocument>("TeamUsers");
        public IMongoCollection<TeamUserJoinRequestDocument> TeamUserJoinRequests => _mongoDatabase.GetCollection<TeamUserJoinRequestDocument>("TeamUserJoinRequests");
        public IMongoCollection<TeamJoinProjectRequestDocument> TeamJoinProjectRequests => _mongoDatabase.GetCollection<TeamJoinProjectRequestDocument>("TeamJoinProjectRequests");
        public IMongoCollection<TeamJoinInvitationDocument> TeamJoinInvitations => _mongoDatabase.GetCollection<TeamJoinInvitationDocument>("TeamJoinInvitations");
        public IMongoCollection<ProjectTeamParticipantDocument> TeamProjects => _mongoDatabase.GetCollection<ProjectTeamParticipantDocument>("TeamProjects");
    }
}