using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.MongoDb;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class TeamParticipantDocumentBuilder
    {
        private string _id = Uuid.NewMongo();
        private string _userId = Uuid.NewMongo();
        private string _username = "Username";
        private string _teamId = Uuid.NewMongo();

        public TeamParticipantDocumentBuilder WithTeamId(string teamId)
        {
            _teamId = teamId;
            return this;
        }

        public TeamParticipantDocumentBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public TeamParticipantDocumentBuilder WithUsername(string username)
        {
            _username = username;
            return this;
        }

        public TeamParticipantDocument Build()
        {
            return TeamParticipantDocument.Create(_id, _userId, _username, _teamId);
        }

        public static implicit operator TeamParticipantDocument(TeamParticipantDocumentBuilder builder)
        {
            return builder.Build();
        }
    }
}