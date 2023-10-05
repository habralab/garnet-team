using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors;

public class TeamNotFoundError : ApplicationError
{
    public TeamNotFoundError(string teamId)
        : base($"Команда с идентификатором '{teamId}' не найдена")
    {
    }

    public override string Code => nameof(TeamNotFoundError);
}