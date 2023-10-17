using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Project.Errors;

public class ProjectNameCanNotBeEmptyError : ApplicationError
{
    public ProjectNameCanNotBeEmptyError() : base("Название проекта не может быть пустым")
    {
    }

    public override string Code => nameof(ProjectNameCanNotBeEmptyError);
}