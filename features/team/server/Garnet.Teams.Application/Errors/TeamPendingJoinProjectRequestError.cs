using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamPendingJoinProjectRequestError : ApplicationError
    {
        public TeamPendingJoinProjectRequestError(string teamId) : base($"Команда с идентификатором '{teamId}' уже направила заявку на вступление в проект")
        {
        }

        public override string Code => nameof(TeamPendingJoinProjectRequestError);
    }
}