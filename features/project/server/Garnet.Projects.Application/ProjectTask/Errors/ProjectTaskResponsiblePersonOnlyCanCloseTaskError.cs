using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskResponsiblePersonOnlyCanCloseTaskError : ApplicationError
{
    public ProjectTaskResponsiblePersonOnlyCanCloseTaskError() : base("Недостаточно полномочий для закрытия задачи")
    {
    }

    public override string Code => nameof(ProjectTaskResponsiblePersonOnlyCanCloseTaskError);
}