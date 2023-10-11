using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Team.Errors
{
    public class TeamOnlyOwnerCanChangeOwnerError : ApplicationError
    {
        public TeamOnlyOwnerCanChangeOwnerError() : base("Изменить владельца команды может только ее владелец")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanChangeOwnerError);
    }
}