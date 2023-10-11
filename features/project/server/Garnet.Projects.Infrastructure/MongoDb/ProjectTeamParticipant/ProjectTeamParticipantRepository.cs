using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.ProjectTeamParticipant;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;

public class ProjectTeamParticipantRepository : IProjectTeamParticipantRepository
{
    private readonly DbFactory _dbFactory;

    private readonly UpdateDefinitionBuilder<ProjectTeamParticipantDocument> _u =
        Builders<ProjectTeamParticipantDocument>.Update;

    private readonly FilterDefinitionBuilder<ProjectTeamParticipantDocument> _f =
        Builders<ProjectTeamParticipantDocument>.Filter;


    public ProjectTeamParticipantRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectTeamParticipantEntity> AddProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName, string projectId)
    {
        var db = _dbFactory.Create();
        var team = ProjectTeamParticipantDocument.Create(Uuid.NewMongo(), teamId, teamName, projectId);
        await db.ProjectTeamsParticipants.InsertOneAsync(team, cancellationToken: ct);

        return ProjectTeamParticipantDocument.ToDomain(team);
    }

    public async Task<ProjectTeamParticipantEntity[]> GetProjectTeamParticipantsByProjectId(CancellationToken ct,
        string projectId)
    {
        var db = _dbFactory.Create();
        var teams = await db.ProjectTeamsParticipants.Find(x => x.ProjectId == projectId)
            .ToListAsync(cancellationToken: ct);

        return teams.Select(ProjectTeamParticipantDocument.ToDomain).ToArray();
    }

    public async Task UpdateProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName)
    {
        var db = _dbFactory.Create();
        await db.ProjectTeamsParticipants.UpdateManyAsync(
            _f.Eq(x => x.TeamId, teamId),
            _u.Set(x => x.TeamName, teamName),
            cancellationToken: ct
        );
    }

    public async Task DeleteProjectTeamParticipantsByProjectId(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();
        await db.ProjectTeamsParticipants.DeleteManyAsync(
            _f.Eq(x => x.ProjectId, projectId),
            cancellationToken: ct
        );
    }
}