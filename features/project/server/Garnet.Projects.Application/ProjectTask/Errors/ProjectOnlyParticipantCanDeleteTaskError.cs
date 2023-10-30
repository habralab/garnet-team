using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectOnlyParticipantCanDeleteTaskError : ApplicationError
{
    public ProjectOnlyParticipantCanDeleteTaskError() : base("Только участник проекта может удалить задачу")
    {
    }

    public override string Code => nameof(ProjectOnlyParticipantCanDeleteTaskError);
}