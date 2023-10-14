using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Team.Errors
{
    public class TeamOnlyOwnerCanChangeName : ApplicationError
    {
        public TeamOnlyOwnerCanChangeName() : base("Изменить название команды может только ее владелец")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanChangeName);
    }
}