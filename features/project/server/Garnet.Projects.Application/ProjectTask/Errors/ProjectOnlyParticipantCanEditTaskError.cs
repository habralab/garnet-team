using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectOnlyParticipantCanEditTaskError : ApplicationError
{
    public ProjectOnlyParticipantCanEditTaskError() : base("Только участник проекта может изменить задачу")
    {
    }

    public override string Code => nameof(ProjectOnlyParticipantCanEditTaskError);
}