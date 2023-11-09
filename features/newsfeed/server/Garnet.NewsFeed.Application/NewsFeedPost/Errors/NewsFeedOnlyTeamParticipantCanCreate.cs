using Garnet.Common.Application.Errors;

namespace Garnet.NewsFeed.Application.NewsFeedPost.Errors
{
    public class NewsFeedOnlyTeamParticipantCanCreate : ApplicationError
    {
        public NewsFeedOnlyTeamParticipantCanCreate() : base($"Создавать посты в ленте команды могут только ее участники")
        {
        }

        public override string Code => nameof(NewsFeedOnlyTeamParticipantCanCreate);
    }
}