using FluentResults;
using Garnet.Common.Infrastructure.Support;
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

    public async Task<ProjectTeamParticipant> AddProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName, string? projectId)
    {
        var db = _dbFactory.Create();
        var team = ProjectTeamParticipantDocument.Create(Uuid.NewMongo(), teamId, teamName, projectId);
        await db.ProjectTeamsParticipants.InsertOneAsync(team, cancellationToken: ct);

        return ProjectTeamParticipantDocument.ToDomain(team);
    }

    public async Task<ProjectTeamParticipant[]> GetProjectTeamParticipantsByProjectId(CancellationToken ct,
        string projectId)
    {
        var db = _dbFactory.Create();
        var teams = await db.ProjectTeamsParticipants.Find(x => x.ProjectId == projectId)
            .ToListAsync(cancellationToken: ct);

        return teams.Select(ProjectTeamParticipantDocument.ToDomain).ToArray();
    }

    public async Task<ProjectTeamParticipant[]> UpdateProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName)
    {
        var db = _dbFactory.Create();
        var teams = await db.ProjectTeamsParticipants.Find(x => x.TeamId == teamId).ToListAsync(cancellationToken: ct);
        foreach (var team in teams)
        {
            team.TeamName = teamName;
            await db.ProjectTeamsParticipants.InsertOneAsync(team, cancellationToken: ct);
        }

        return teams.Select(ProjectTeamParticipantDocument.ToDomain).ToArray();
    }
}