namespace Garnet.Projects.Application.ProjectTeamJoinRequest;

public interface IProjectTeamJoinRequestRepository
{
    Task<ProjectTeamJoinRequestEntity> AddProjectTeamJoinRequest(CancellationToken ct, string id, string teamId, string teamName, string projectId);
    Task<ProjectTeamJoinRequestEntity[]> GetProjectTeamJoinRequestsByProjectId(CancellationToken ct, string projectId);
    Task<ProjectTeamJoinRequestEntity?> GetProjectTeamJoinRequestById(CancellationToken ct, string projectTeamJoinRequestId);
    Task<ProjectTeamJoinRequestEntity?> DeleteProjectTeamJoinRequestById(CancellationToken ct, string projectTeamJoinRequestId);
    Task UpdateProjectTeamJoinRequest(CancellationToken ct,string teamId, string teamName);
}