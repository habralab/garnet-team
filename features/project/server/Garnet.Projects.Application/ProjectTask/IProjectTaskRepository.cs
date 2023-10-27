using Garnet.Projects.Application.ProjectTask.Args;

namespace Garnet.Projects.Application.ProjectTask;

public interface IProjectTaskRepository
{
    Task<ProjectTaskEntity> CreateProjectTask(CancellationToken ct, string userCreatorId, string status, int taskNumber,
        ProjectTaskCreateArgs args);
}