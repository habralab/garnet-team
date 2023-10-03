using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Errors;

public class ProjectNotFoundError : ApplicationError
{
    public ProjectNotFoundError(string projectId)
        : base($"Проект с идентификатором '{projectId}' не найден")
    {
    }

    public override string Code => nameof(ProjectNotFoundError);
}