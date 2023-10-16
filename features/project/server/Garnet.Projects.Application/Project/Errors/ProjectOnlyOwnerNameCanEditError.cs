using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Project.Errors;

internal class ProjectOnlyOwnerNameCanEditError : ApplicationError
{
    public ProjectOnlyOwnerNameCanEditError() : base("Название проекта может отредактировать только его владелец")
    {
    }

    public override string Code => nameof(ProjectOnlyOwnerNameCanEditError);
}