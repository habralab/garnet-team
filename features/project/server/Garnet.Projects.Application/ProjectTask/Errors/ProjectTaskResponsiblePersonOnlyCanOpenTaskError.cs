using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskResponsiblePersonOnlyCanOpenTaskError : ApplicationError
{
    public ProjectTaskResponsiblePersonOnlyCanOpenTaskError() : base("Недостаточно полномочий для открытия задачи")
    {
    }

    public override string Code => nameof(ProjectTaskResponsiblePersonOnlyCanOpenTaskError);
}