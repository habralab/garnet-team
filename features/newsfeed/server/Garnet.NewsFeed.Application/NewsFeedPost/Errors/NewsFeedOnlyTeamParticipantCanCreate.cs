using Garnet.Common.Application.Errors;

namespace Garnet.NewsFeed.Application.NewsFeedPost.Errors
{
    public class NewsFeedOnlyTeamParticipantCanCreate : ApplicationError
    {
        public NewsFeedOnlyTeamParticipantCanCreate() : base($"Только владелец и участники могут создавать посты в команде")
        {
        }

        public override string Code => nameof(NewsFeedOnlyTeamParticipantCanCreate);
    }
}