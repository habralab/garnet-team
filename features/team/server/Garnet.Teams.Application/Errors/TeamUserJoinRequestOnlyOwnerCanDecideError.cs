using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamUserJoinRequestOnlyOwnerCanDecideError : ApplicationError
    {
        public TeamUserJoinRequestOnlyOwnerCanDecideError() : base("Выносить вердикт по заявкам на вступление может только владелец этой команды")
        {
        }

        public override string Code => nameof(TeamUserJoinRequestOnlyOwnerCanDecideError);
    }
}