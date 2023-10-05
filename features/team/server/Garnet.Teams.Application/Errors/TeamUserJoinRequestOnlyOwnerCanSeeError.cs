using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamUserJoinRequestOnlyOwnerCanSeeError : ApplicationError
    {
        public TeamUserJoinRequestOnlyOwnerCanSeeError() : base("Просматривать заявки на вступление может только владелец этой команды")
        {
        }

        public override string Code => nameof(TeamUserJoinRequestOnlyOwnerCanSeeError);
    }
}