namespace Garnet.Projects.Application.ProjectUser.Queries;

public class ProjectUserGetQuery
{
    private readonly IProjectUserRepository _projectUserRepository;

    public ProjectUserGetQuery(IProjectUserRepository projectUserRepository)
    {
        _projectUserRepository = projectUserRepository;
    }

    public async Task<ProjectUserEntity?> Query(CancellationToken ct, string userId)
    {
        return await _projectUserRepository.GetUser(ct, userId);
    }
}