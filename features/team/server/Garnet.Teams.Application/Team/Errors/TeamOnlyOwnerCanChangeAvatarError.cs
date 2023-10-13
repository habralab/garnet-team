using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Team.Errors
{
    public class TeamOnlyOwnerCanChangeAvatarError : ApplicationError
    {
        public TeamOnlyOwnerCanChangeAvatarError() : base("Изменить аватар команды может только ее владелец")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanChangeAvatarError);
    }
}