using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectOnlyParticipantCanSetAsTaskExecutorError : ApplicationError
{
    public ProjectOnlyParticipantCanSetAsTaskExecutorError() : base("Исполнителем задачи можно назначить только участника проекта")
    {
    }

    public override string Code => nameof(ProjectOnlyParticipantCanSetAsTaskExecutorError);
}