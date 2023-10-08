namespace Garnet.Projects.Application;

public interface IProjectTeamJoinRequestRepository
{
    Task<ProjectTeamJoinRequest> AddProjectTeamJoinRequest(CancellationToken ct, string teamId, string teamName, string projectId);
    Task<ProjectTeamJoinRequest[]> GetProjectTeamJoinRequestsByProjectId(CancellationToken ct, string projectId);
    Task<ProjectTeamJoinRequest?> GetProjectTeamJoinRequestById(CancellationToken ct, string projectTeamJoinRequestId);
    Task<ProjectTeamJoinRequest?> DeleteProjectTeamJoinRequestById(CancellationToken ct, string projectTeamJoinRequestId);
    Task UpdateProjectTeamJoinRequest(CancellationToken ct,string teamId, string teamName);
}