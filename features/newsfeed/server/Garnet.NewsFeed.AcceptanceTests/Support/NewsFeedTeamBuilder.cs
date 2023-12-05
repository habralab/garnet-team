using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeam;

namespace Garnet.NewsFeed.AcceptanceTests.Support
{
    public class NewsFeedTeamBuilder
    {
        private string _id = Uuid.NewMongo();
        private string _ownerUserId = "system";

        public NewsFeedTeamBuilder WithOwner(string ownerUserId)
        {
            _ownerUserId = ownerUserId;
            return this;
        }

        public NewsFeedTeamBuilder WithTeamId(string teamId)
        {
            _id = teamId;
            return this;
        }

        public NewsFeedTeamDocument Build()
        {
            return NewsFeedTeamDocument.Create(_id, _ownerUserId);
        }

        public static implicit operator NewsFeedTeamDocument(NewsFeedTeamBuilder builder)
        {
            return builder.Build();
        }
    }
}