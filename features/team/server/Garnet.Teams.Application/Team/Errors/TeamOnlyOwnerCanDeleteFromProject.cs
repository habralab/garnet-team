using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Team.Errors
{
    public class TeamOnlyOwnerCanDeleteFromProject : ApplicationError
    {
        public TeamOnlyOwnerCanDeleteFromProject() : base("Только владелец команды может удалить команду из проекта")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanDeleteFromProject);
    }
}