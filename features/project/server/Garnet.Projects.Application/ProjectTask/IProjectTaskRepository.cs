using Garnet.Projects.Application.ProjectTask.Args;

namespace Garnet.Projects.Application.ProjectTask;

public interface IProjectTaskRepository
{
    Task<ProjectTaskEntity> CreateProjectTask(CancellationToken ct, string responsibleUserId, string status,
        int taskNumber,
        ProjectTaskCreateArgs args);

    Task<ProjectTaskEntity?> GetProjectTaskById(CancellationToken ct, string taskId);
    Task<ProjectTaskEntity> DeleteProjectTask(CancellationToken ct, string taskId);
    Task<ProjectTaskEntity> EditProjectTaskName(CancellationToken ct, string taskId, string newTaskName);
    Task<ProjectTaskEntity> EditProjectTaskResponsibleUser(CancellationToken ct, string taskId, string newResponsibleUserId);
    Task<ProjectTaskEntity> EditProjectTaskDescription(CancellationToken ct, string taskId, string description);
    Task<ProjectTaskEntity> EditProjectTaskTags(CancellationToken ct, string taskId, string[] tags);
    Task<ProjectTaskEntity> EditProjectTaskLabels(CancellationToken ct, string taskId, string[] labels);
    Task<ProjectTaskEntity> CloseProjectTask(CancellationToken ct, string taskId, string status);
    Task CreateIndexes(CancellationToken ct);
}