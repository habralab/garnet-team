using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskParticipantOnlyCanSetAsResponsibleUserError : ApplicationError
{
    public ProjectTaskParticipantOnlyCanSetAsResponsibleUserError() : base("Только участник проекта может быть выбран отвественным за задачу")
    {
    }

    public override string Code => nameof(ProjectTaskParticipantOnlyCanSetAsResponsibleUserError);
}