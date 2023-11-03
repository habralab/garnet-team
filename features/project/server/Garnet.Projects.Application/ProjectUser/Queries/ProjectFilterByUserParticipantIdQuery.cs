using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application.ProjectUser.Queries;

public class ProjectFilterByUserParticipantIdQuery
{
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public ProjectFilterByUserParticipantIdQuery(IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }

    public async Task<ProjectEntity[]> Query(CancellationToken ct, string userId)
    {
        return await _projectTeamParticipantRepository.GetProjectsOfUserParticipantByUserId(ct, userId);
    }
}