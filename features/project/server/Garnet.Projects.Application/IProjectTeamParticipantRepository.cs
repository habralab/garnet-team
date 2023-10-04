namespace Garnet.Projects.Application;

public interface IProjectTeamParticipantRepository
{
    Task<ProjectTeamParticipant> AddProjectTeamParticipant(CancellationToken ct, string id, string teamId, string projectId);
    Task<ProjectTeamParticipant[]> GetProjectTeamParticipantByProjectId(CancellationToken ct, string projectId);
}