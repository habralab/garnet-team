using Garnet.Projects.Application.ProjectTeam;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTeam;

public class ProjectTeamRepository : IProjectTeamRepository
{
    private readonly DbFactory _dbFactory;
    private readonly FilterDefinitionBuilder<ProjectTeamDocument> _f = Builders<ProjectTeamDocument>.Filter;
    private readonly UpdateDefinitionBuilder<ProjectTeamDocument> _u = Builders<ProjectTeamDocument>.Update;


    public ProjectTeamRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectTeamEntity> AddProjectTeam(CancellationToken ct, string teamId,
        string teamName, string ownerUserId)
    {
        var db = _dbFactory.Create();
        var team = ProjectTeamDocument.Create(teamId, teamName, ownerUserId);
        await db.ProjectTeams.InsertOneAsync(team, cancellationToken: ct);

        return ProjectTeamDocument.ToDomain(team);
    }


    public async Task<ProjectTeamEntity> UpdateProjectTeam(CancellationToken ct, string teamId,
        string teamName, string ownerUserId)
    {
        var db = _dbFactory.Create();
        var team = await db.ProjectTeams.FindOneAndUpdateAsync(
            _f.Eq(x => x.Id, teamId),
            _u.Set(x => x.TeamName, teamName)
                .Set(x => x.OwnerUserId, ownerUserId),
            options: new FindOneAndUpdateOptions<ProjectTeamDocument>
            {
                ReturnDocument = ReturnDocument.After
            },
            cancellationToken: ct
        );


        return ProjectTeamDocument.ToDomain(team);
    }
}