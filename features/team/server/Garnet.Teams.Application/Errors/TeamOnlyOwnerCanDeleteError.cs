using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors;

public class TeamOnlyOwnerCanDeleteError : ApplicationError
{
    public TeamOnlyOwnerCanDeleteError() : base("Команду может удалить только ее владелец")
    {
    }

    public override string Code => nameof(TeamOnlyOwnerCanDeleteError);
}