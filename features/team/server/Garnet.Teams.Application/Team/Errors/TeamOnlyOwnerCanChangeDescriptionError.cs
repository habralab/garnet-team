using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Team.Errors
{
    public class TeamOnlyOwnerCanChangeDescriptionError : ApplicationError
    {
        public TeamOnlyOwnerCanChangeDescriptionError() : base("Изменить описание команды может только ее владелец")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanChangeDescriptionError);
    }
}