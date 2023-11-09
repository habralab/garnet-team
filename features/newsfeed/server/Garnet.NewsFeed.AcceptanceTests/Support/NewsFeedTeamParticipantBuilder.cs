using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeamParticipant;

namespace Garnet.NewsFeed.AcceptanceTests.Support
{
    public class NewsFeedTeamParticipantBuilder
    {
        private string _id = Uuid.NewMongo();
        private string _userId = "system";
        private string _teamId = "TeamId";

        public NewsFeedTeamParticipantBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public NewsFeedTeamParticipantBuilder WithTeamId(string teamId)
        {
            _teamId = teamId;
            return this;
        }

        public NewsFeedTeamParticipantDocument Build()
        {
            return NewsFeedTeamParticipantDocument.Create(_id, _teamId, _userId);
        }

        public static implicit operator NewsFeedTeamParticipantDocument(NewsFeedTeamParticipantBuilder builder)
        {
            return builder.Build();
        }
    }
}