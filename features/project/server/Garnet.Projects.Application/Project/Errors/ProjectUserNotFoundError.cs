using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Project.Errors;

public class ProjectUserNotFoundError : ApplicationError
{
    public ProjectUserNotFoundError(string userId)
        : base($"Пользователь с идентификатором '{userId}' не найден")
    {
    }

    public override string Code => nameof(ProjectUserNotFoundError);
}