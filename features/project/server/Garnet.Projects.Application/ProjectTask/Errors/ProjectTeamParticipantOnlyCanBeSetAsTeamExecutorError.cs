using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTeamParticipantOnlyCanBeSetAsTeamExecutorError : ApplicationError
{
    public ProjectTeamParticipantOnlyCanBeSetAsTeamExecutorError() : base(
        "Исполнителем задачи можно назначить только команду, которая является участником проекта")
    {
    }

    public override string Code => nameof(ProjectTeamParticipantOnlyCanBeSetAsTeamExecutorError);
}