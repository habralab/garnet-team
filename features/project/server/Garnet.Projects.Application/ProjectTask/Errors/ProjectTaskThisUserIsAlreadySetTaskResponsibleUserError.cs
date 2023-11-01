using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskThisUserIsAlreadySetTaskResponsibleUserError : ApplicationError
{
    public ProjectTaskThisUserIsAlreadySetTaskResponsibleUserError() : base(
        "Этот пользователь уже является отвественным за задачу")
    {
    }

    public override string Code => nameof(ProjectTaskThisUserIsAlreadySetTaskResponsibleUserError);
}