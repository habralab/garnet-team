using Garnet.NewsFeed.Application.NewsFeedTeam;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeam
{
    public record NewsFeedTeamDocument
    {
        public string Id { get; init; } = null!;
        public string OwnerUserId { get; init; } = null!;

        public static NewsFeedTeamDocument Create(string id, string ownerUserId)
        {
            return new NewsFeedTeamDocument()
            {
                Id = id,
                OwnerUserId = ownerUserId
            };
        }

        public static NewsFeedTeamEntity ToDomain(NewsFeedTeamDocument doc)
        {
            return new NewsFeedTeamEntity(doc.Id, doc.OwnerUserId);
        }
    }
}