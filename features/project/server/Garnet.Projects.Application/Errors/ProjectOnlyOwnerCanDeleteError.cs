using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Errors;

public class ProjectOnlyOwnerCanDeleteError : ApplicationError
{
    public ProjectOnlyOwnerCanDeleteError() : base("Проект может удалить только его владелец")
    {
    }

    public override string Code => nameof(ProjectOnlyOwnerCanDeleteError);
}