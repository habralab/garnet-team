using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Errors
{
    public class TeamUserJoinRequestOnlyAuthorCanCancelError : ApplicationError
    {
        public TeamUserJoinRequestOnlyAuthorCanCancelError() : base("Отменить заявку на вступление в команду может только ее автор")
        {
        }

        public override string Code => nameof(TeamUserJoinRequestOnlyAuthorCanCancelError);
    }
}