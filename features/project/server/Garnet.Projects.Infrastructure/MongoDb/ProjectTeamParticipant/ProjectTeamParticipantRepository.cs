using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;

public class ProjectTeamParticipantRepository : IProjectTeamParticipantRepository
{
    private readonly DbFactory _dbFactory;

    private readonly IndexKeysDefinitionBuilder<ProjectTeamParticipantDocument> _i =
        Builders<ProjectTeamParticipantDocument>.IndexKeys;

    private readonly UpdateDefinitionBuilder<ProjectTeamParticipantDocument> _u =
        Builders<ProjectTeamParticipantDocument>.Update;

    private readonly FilterDefinitionBuilder<ProjectTeamParticipantDocument> _teamParticipantFilter =
        Builders<ProjectTeamParticipantDocument>.Filter;

    private readonly FilterDefinitionBuilder<ProjectUserDocument> _userFilter =
        Builders<ProjectUserDocument>.Filter;

    private readonly FilterDefinitionBuilder<ProjectDocument> _projectFilter =
        Builders<ProjectDocument>.Filter;

    public ProjectTeamParticipantRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectTeamParticipantEntity> AddProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName, string projectId)
    {
        var db = _dbFactory.Create();
        var team = await db.ProjectTeams.Find(x => x.Id == teamId).FirstAsync(ct);
        var userParticipantsIds = team.UserParticipantIds;
        var userParticipants =
            await db.ProjectUsers.Find(_userFilter.In(x => x.Id, userParticipantsIds)).ToListAsync(ct);

        var projectsIdOfTeamParticipant =
            await db.ProjectTeamsParticipants.Find(x => x.TeamId == teamId).ToListAsync(ct);
        var projectIds = projectsIdOfTeamParticipant.Select(x => x.ProjectId);
        var projectsList =
            await db.Projects.Find(_projectFilter.In(x => x.Id, projectIds)).ToListAsync(ct);

        var teamParticipant = ProjectTeamParticipantDocument.Create(Uuid.NewMongo(), teamId, teamName, projectId,
            team.TeamAvatarUrl,
            userParticipants.ToArray(), projectsList.ToArray());
        await db.ProjectTeamsParticipants.InsertOneAsync(teamParticipant, cancellationToken: ct);

        return ProjectTeamParticipantDocument.ToDomain(teamParticipant);
    }

    public async Task<ProjectTeamParticipantEntity[]> GetProjectTeamParticipantsByProjectId(CancellationToken ct,
        string projectId)
    {
        var db = _dbFactory.Create();
        var teams = await db.ProjectTeamsParticipants.Find(x => x.ProjectId == projectId)
            .ToListAsync(cancellationToken: ct);

        return teams.Select(ProjectTeamParticipantDocument.ToDomain).ToArray();
    }

    public async Task<ProjectEntity[]> GetProjectsOfUserParticipantByUserId(CancellationToken ct,
        string userId)
    {
        var db = _dbFactory.Create();
        var teamParticipants = await db.ProjectTeamsParticipants.Find(
                _teamParticipantFilter.Where(x => x.UserParticipants.Any(user => user.Id == userId)))
            .ToListAsync(cancellationToken: ct);
        var projectIds = teamParticipants.Select(x => x.ProjectId).ToArray();

        var projectEntities = await db.Projects.Find(
            _projectFilter.In(x => x.Id, projectIds)).ToListAsync(ct);

        return projectEntities.Select(ProjectDocument.ToDomain).ToArray();
    }

    public async Task UpdateProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName, string? teamAvatarUrl)
    {
        var db = _dbFactory.Create();
        await db.ProjectTeamsParticipants.UpdateManyAsync(
            _teamParticipantFilter.Eq(x => x.TeamId, teamId),
            _u.Set(x => x.TeamName, teamName)
                .Set(x => x.TeamAvatarUrl, teamAvatarUrl),
            cancellationToken: ct
        );
    }

    public async Task DeleteProjectTeamParticipantsByProjectId(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();
        await db.ProjectTeamsParticipants.DeleteManyAsync(
            _teamParticipantFilter.Eq(x => x.ProjectId, projectId),
            cancellationToken: ct
        );
    }

    public async Task AddProjectTeamUserParticipant(CancellationToken ct, string teamId, string userId)
    {
        var db = _dbFactory.Create();
        var user = await db.ProjectUsers.Find(x => x.Id == userId).FirstAsync(ct);

        await db.ProjectTeamsParticipants.UpdateManyAsync(
            _teamParticipantFilter.Eq(x => x.TeamId, teamId),
            _u.AddToSet(x => x.UserParticipants, user),
            cancellationToken: ct
        );
    }

    public async Task CreateIndexes(CancellationToken ct)
    {
        var db = _dbFactory.Create();
        await db.ProjectTeamsParticipants.Indexes.CreateOneAsync(
            new CreateIndexModel<ProjectTeamParticipantDocument>(
                _i.Text(o => o.TeamName)
            ),
            cancellationToken: ct);
    }

    public async Task<ProjectTeamParticipantEntity?> DeleteProjectTeamParticipantsByTeamId(CancellationToken ct,
        string teamId)
    {
        var db = _dbFactory.Create();
        var teamParticipant = await db.ProjectTeamsParticipants.FindOneAndDeleteAsync(
            _teamParticipantFilter.Eq(x => x.TeamId, teamId)
        );

        return teamParticipant is null ? null : ProjectTeamParticipantDocument.ToDomain(teamParticipant);
    }
}