namespace Garnet.Projects.Application.ProjectUser.Commands;

public class ProjectUserUpdateCommand
{
    private readonly IProjectUserRepository _projectUserRepository;

    public ProjectUserUpdateCommand(IProjectUserRepository projectUserRepository)
    {
        _projectUserRepository = projectUserRepository;
    }

    public async Task Execute(CancellationToken ct, string userId, string userName, string userAvatarUrl)
    {
        await _projectUserRepository.UpdateUser(ct, userId, userName, userAvatarUrl);
    }
}