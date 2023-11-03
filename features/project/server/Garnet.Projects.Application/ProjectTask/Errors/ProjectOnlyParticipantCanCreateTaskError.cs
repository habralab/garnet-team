using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectOnlyParticipantCanCreateTaskError : ApplicationError
{
    public ProjectOnlyParticipantCanCreateTaskError() : base("Только участник проекта может создать задачу")
    {
    }

    public override string Code => nameof(ProjectOnlyParticipantCanCreateTaskError);
}