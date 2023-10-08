using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamUserJoinRequestNotFoundError : ApplicationError
    {
        public TeamUserJoinRequestNotFoundError(string userJoinRequestId) : 
        base($"Заявка с идентификатором '{userJoinRequestId}' на вступление в команду не найдена")
        {
        }

        public override string Code => nameof(TeamUserJoinRequestNotFoundError);
    }
}