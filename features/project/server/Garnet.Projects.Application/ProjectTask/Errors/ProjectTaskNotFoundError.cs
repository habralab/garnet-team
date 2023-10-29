using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskNotFoundError : ApplicationError
{
    public ProjectTaskNotFoundError(string taskId)
        : base($"Задача с идентификатором '{taskId}' не найдена")
    {
    }

    public override string Code => nameof(ProjectTaskNotFoundError);
}