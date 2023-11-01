using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectOnlyTeamParticipantCanSetAsTaskExecutorError : ApplicationError
{
    public ProjectOnlyTeamParticipantCanSetAsTaskExecutorError() : base("Исполнителем задачи можно назначить только команду, которая является участником проекта")
    {
    }

    public override string Code => nameof(ProjectOnlyTeamParticipantCanSetAsTaskExecutorError);
}