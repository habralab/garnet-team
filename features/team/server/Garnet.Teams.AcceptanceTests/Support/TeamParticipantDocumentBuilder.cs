using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.MongoDb;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class TeamParticipantDocumentBuilder
    {
        private string _id = Uuid.NewMongo();
        private string _userId = Uuid.NewMongo();
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

        public TeamParticipantDocument Build()
        {
            return TeamParticipantDocument.Create(_id, _userId, _teamId);
        }

        public static implicit operator TeamParticipantDocument(TeamParticipantDocumentBuilder builder)
        {
            return builder.Build();
        }
    }
}