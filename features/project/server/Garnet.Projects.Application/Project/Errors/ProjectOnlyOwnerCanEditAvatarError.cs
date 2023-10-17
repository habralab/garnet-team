using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Project.Errors;

public class ProjectOnlyOwnerCanEditAvatarError : ApplicationError
{
    public ProjectOnlyOwnerCanEditAvatarError() : base("Изменить аватар проекта может только его владелец")
    {
    }

    public override string Code => nameof(ProjectOnlyOwnerCanEditAvatarError);
}