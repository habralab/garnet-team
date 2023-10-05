using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Errors;

public class ProjectOnlyOwnerCanEditError : ApplicationError
{
    public ProjectOnlyOwnerCanEditError() : base("Проект может отредактировать только его владелец")
    {
    }

    public override string Code => nameof(ProjectOnlyOwnerCanEditError);
}