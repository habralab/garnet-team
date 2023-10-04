using Garnet.Projects.Application;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.Migrations;

public class ProjectTeamParticipantRepository : IProjectTeamParticipantRepository
{
    private readonly DbFactory _dbFactory;

    public ProjectTeamParticipantRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectTeamParticipant> AddProjectTeamParticipant(CancellationToken ct, string id, string teamId, string projectId)
    {
        var db = _dbFactory.Create();
        var team = ProjectTeamParticipantDocument.Create(id, teamId, projectId);
        await db.ProjectTeamsParticipants.InsertOneAsync(team, cancellationToken: ct);

        return ProjectTeamParticipantDocument.ToDomain(team);
    }

    public async Task<ProjectTeamParticipant[]> GetProjectTeamParticipantByProjectId(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();
        var teams = await db.ProjectTeamsParticipants.Find(x => x.ProjectId == projectId).ToListAsync();

        return teams.Select(ProjectTeamParticipantDocument.ToDomain).ToArray();
    }
}