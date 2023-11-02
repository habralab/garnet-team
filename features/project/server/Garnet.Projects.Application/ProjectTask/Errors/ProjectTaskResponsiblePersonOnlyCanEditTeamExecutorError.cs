using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskResponsiblePersonOnlyCanEditTeamExecutorError : ApplicationError
{
    public ProjectTaskResponsiblePersonOnlyCanEditTeamExecutorError() : base("Недостаточно полномочий для изменения команды исполнителей задачи")
    {
    }

    public override string Code => nameof(ProjectTaskResponsiblePersonOnlyCanEditTeamExecutorError);
}