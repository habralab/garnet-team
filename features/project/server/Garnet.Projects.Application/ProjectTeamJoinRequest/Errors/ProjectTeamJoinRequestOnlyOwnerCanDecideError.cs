using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Errors;

public class ProjectTeamJoinRequestOnlyOwnerCanDecideError : ApplicationError
{
    public ProjectTeamJoinRequestOnlyOwnerCanDecideError()
        : base("Выносить вердикт по заявкам на вступление может только владелец этого проекта")
    {
    }

    public override string Code => nameof(ProjectTeamJoinRequestOnlyOwnerCanDecideError);
}