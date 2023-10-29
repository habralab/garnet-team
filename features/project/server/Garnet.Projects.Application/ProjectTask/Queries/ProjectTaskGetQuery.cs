using FluentResults;
using Garnet.Projects.Application.ProjectTask.Errors;

namespace Garnet.Projects.Application.ProjectTask.Queries;

public class ProjectTaskGetQuery
{
    private readonly IProjectTaskRepository _projectTaskRepository;

    public ProjectTaskGetQuery(IProjectTaskRepository projectTaskRepository)
    {
        _projectTaskRepository = projectTaskRepository;
    }

    public async Task<Result<ProjectTaskEntity>> Query(CancellationToken ct, string taskId)
    {
        var task = await _projectTaskRepository.GetProjectTaskById(ct, taskId);
        return task is null ? Result.Fail(new ProjectTaskNotFoundError(taskId)) : Result.Ok(task);
    }
}