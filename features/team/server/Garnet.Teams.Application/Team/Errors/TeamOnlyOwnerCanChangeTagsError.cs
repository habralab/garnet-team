using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Team.Errors
{
    public class TeamOnlyOwnerCanChangeTagsError : ApplicationError
    {
        public TeamOnlyOwnerCanChangeTagsError() : base("Изменить теги команды может только ее владелец")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanChangeTagsError);
    }
}