using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskAlreadySetThisStatusError : ApplicationError
{
    public ProjectTaskAlreadySetThisStatusError(string status) : base($"У задачи уже установлен статус {status}")
    {
    }

    public override string Code => nameof(ProjectTaskAlreadySetThisStatusError);
}