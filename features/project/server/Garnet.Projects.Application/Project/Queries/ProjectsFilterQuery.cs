using FluentResults;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Args;
using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Application.Project.Queries;

public class ProjectsFilterQuery
{
    private readonly IProjectRepository _projectRepository;


    public ProjectsFilterQuery(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectEntity[]>> Execute(CancellationToken ct, ProjectFilterArgs args)
    {
        return await _projectRepository.FilterProjects(ct, args);
    }
}