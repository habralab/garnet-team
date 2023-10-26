namespace Garnet.Projects.Application.ProjectUser.Commands;

public class ProjectUserCreateCommand
{
    private readonly IProjectUserRepository _projectUserRepository;

    public ProjectUserCreateCommand(IProjectUserRepository projectUserRepository)
    {
        _projectUserRepository = projectUserRepository;
    }

    public async Task<ProjectUserEntity> Execute(CancellationToken ct, string userId, string userName)
    {
        return await _projectUserRepository.AddUser(ct, userId, userName);
    }
}