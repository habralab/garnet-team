using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.Errors;

public class ProjectTeamJoinRequestNotFoundError : ApplicationError
{
    public ProjectTeamJoinRequestNotFoundError(string teamJoinRequestId)
        : base($"Заявка с идентификатором '{teamJoinRequestId}' на вступление в проект не найдена")
    {
    }

    public override string Code => nameof(ProjectTeamJoinRequestNotFoundError);
}