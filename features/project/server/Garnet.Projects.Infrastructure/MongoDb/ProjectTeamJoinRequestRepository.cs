using FluentResults;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.Migrations;

public class ProjectTeamJoinRequestRepository : IProjectTeamJoinRequestRepository
{
    private readonly DbFactory _dbFactory;

    private readonly UpdateDefinitionBuilder<ProjectTeamJoinRequestDocument> _u =
        Builders<ProjectTeamJoinRequestDocument>.Update;

    private readonly FilterDefinitionBuilder<ProjectTeamJoinRequestDocument> _f =
        Builders<ProjectTeamJoinRequestDocument>.Filter;

    public ProjectTeamJoinRequestRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectTeamJoinRequest> AddProjectTeamJoinRequest(CancellationToken ct, string teamId,
        string teamName, string projectId)
    {
        var db = _dbFactory.Create();
        var teamJoinRequest = ProjectTeamJoinRequestDocument.Create(Uuid.NewMongo(), teamId, teamName, projectId);
        await db.ProjectTeamJoinRequests.InsertOneAsync(teamJoinRequest, cancellationToken: ct);

        return ProjectTeamJoinRequestDocument.ToDomain(teamJoinRequest);
    }

    public async Task<ProjectTeamJoinRequest[]> GetProjectTeamJoinRequestsByProjectId(CancellationToken ct,
        string projectId)
    {
        var db = _dbFactory.Create();
        var teams = await db.ProjectTeamJoinRequests.Find(x => x.ProjectId == projectId)
            .ToListAsync(cancellationToken: ct);

        return teams.Select(ProjectTeamJoinRequestDocument.ToDomain).ToArray();
    }

    public async Task UpdateProjectTeamJoinRequest(CancellationToken ct, string teamId,
        string teamName)
    {
        var db = _dbFactory.Create();
        await db.ProjectTeamJoinRequests.UpdateManyAsync(
            _f.Eq(x => x.TeamId, teamId),
            _u.Set(x => x.TeamName, teamName),
            cancellationToken: ct
        );
    }
}