namespace Garnet.Teams.Application.ProjectTeamParticipant
{
    public interface ITeamProjectRepository
    {
        Task<ProjectTeamParticipantEntity> AddTeamProject(CancellationToken ct, string projectId, string teamId);
        Task<ProjectTeamParticipantEntity[]> TeamProjectListOfTeams(CancellationToken ct, string[] teamIds);
        Task<ProjectTeamParticipantEntity?> RemoveTeamProjectInTeam(CancellationToken ct, string projectId, string teamId);
        Task DeleteAllTeamProjectByTeam(CancellationToken ct, string teamId);
        Task DeleteAllTeamProjectByProject(CancellationToken ct, string projectId);
    }
}