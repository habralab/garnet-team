using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskNameCanNotBeEmptyError : ApplicationError
{
    public ProjectTaskNameCanNotBeEmptyError() : base("Название задачи не может быть пустым")
    {
    }

    public override string Code => nameof(ProjectTaskNameCanNotBeEmptyError);
}