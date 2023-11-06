namespace Garnet.Teams.Application.TeamProject
{
    public interface ITeamProjectRepository
    {
        Task<TeamProject> AddTeamProject(CancellationToken ct, string projectId, string teamId);
        Task<TeamProject[]> GetTeamProjectByTeam(CancellationToken ct, string teamId);
        Task<TeamProject> RemoveTeamProjectInTeam(CancellationToken ct, string projectId, string teamId);
        Task DeleteAllTeamProjectByTeam(CancellationToken ct, string teamId);
        Task DeleteAllTeamProjectByProject(CancellationToken ct, string projectId);
    }
}