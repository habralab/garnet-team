﻿using Garnet.Common.Application.Errors;

namespace Garnet.Projects.Application.ProjectTask.Errors;

public class ProjectTaskResponsiblePersonOnlyCanEditResponsibleUserError : ApplicationError
{
    public ProjectTaskResponsiblePersonOnlyCanEditResponsibleUserError() : base("Недостаточно полномочий для изменения отвественного по задаче")
    {
    }

    public override string Code => nameof(ProjectTaskResponsiblePersonOnlyCanEditResponsibleUserError);
}