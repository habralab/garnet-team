namespace Garnet.Projects.Application;

public interface IProjectTeamJoinRequestRepository
{
    Task<ProjectTeamJoinRequest> AddProjectTeamJoinRequest(CancellationToken ct, string teamId, string teamName, string projectId);
    Task UpdateProjectTeamJoinRequest(CancellationToken ct,string teamId, string teamName);
}