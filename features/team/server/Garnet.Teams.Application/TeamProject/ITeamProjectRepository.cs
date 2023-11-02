namespace Garnet.Teams.Application.TeamProject
{
    public interface ITeamProjectRepository
    {
        Task<TeamProjectEntity> AddTeamProject(CancellationToken ct, string projectId, string teamId);
        Task<TeamProjectEntity[]> TeamProjectListOfTeams(CancellationToken ct, string[] teamIds);
        Task<TeamProjectEntity?> RemoveTeamProjectInTeam(CancellationToken ct, string projectId, string teamId);
        Task DeleteAllTeamProjectByTeam(CancellationToken ct, string teamId);
        Task DeleteAllTeamProjectByProject(CancellationToken ct, string projectId);
    }
}