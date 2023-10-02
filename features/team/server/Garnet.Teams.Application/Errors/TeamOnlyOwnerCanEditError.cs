using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamOnlyOwnerCanEditError : ApplicationError
    {
        public TeamOnlyOwnerCanEditError() : base("Команду может отредактировать только ее владелец")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanEditError);
    }
}