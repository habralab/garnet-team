using Garnet.Common.Application.Errors;

namespace Garnet.NewsFeed.Application.NewsFeedTeam.Errors
{
    public class NewsFeedTeamNotFoundError : ApplicationError
    {
        public NewsFeedTeamNotFoundError(string teamId) : base($"Команда с идентификатором {teamId} не найдена")
        {
        }

        public override string Code => nameof(NewsFeedTeamNotFoundError);
    }
}