using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Project.Errors;

internal class ProjectOnlyNameCanEditError : ApplicationError
{
    public ProjectOnlyNameCanEditError() : base("Название проекта может отредактировать только его владелец")
    {
    }

    public override string Code => nameof(ProjectOnlyNameCanEditError);
}