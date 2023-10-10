using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Project.Errors;

public class ProjectOnlyOwnerCanGetTeamJoinRequestsError : ApplicationError
{
    public ProjectOnlyOwnerCanGetTeamJoinRequestsError() : base("Просматривать заявки на участие в проекте может только владелец проекта")
    {
    }

    public override string Code => nameof(ProjectOnlyOwnerCanGetTeamJoinRequestsError);
}