using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Application.ProjectUser;
using Garnet.Projects.Infrastructure.MongoDb.Project;
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
        var team = await db.ProjectTeams.Find(x => x.Id == teamId).FirstAsync(ct);
        var userParticipantsIds = team.UserParticipantsId;

        var userParticipants = new List<ProjectUserEntity>();
        foreach (var userId in userParticipantsIds)
        {
            var user = await db.ProjectUsers.Find(x => x.Id == userId).FirstAsync(ct);
            userParticipants.Add(new ProjectUserEntity(user.Id, user.UserName, user.UserAvatarUrl));
        }

        var projectsIdOfTeamParticipant =
            await db.ProjectTeamsParticipants.Find(x => x.TeamId == teamId).ToListAsync(ct);
        var projectsList = new List<ProjectEntity>();
        foreach (var projectIdOfTeamParticipant in projectsIdOfTeamParticipant)
        {
            var project = await db.Projects.Find(x => x.Id == projectIdOfTeamParticipant.ProjectId).FirstAsync(ct);
            projectsList.Add(ProjectDocument.ToDomain(project));
        }

        var teamParticipant = ProjectTeamParticipantDocument.Create(Uuid.NewMongo(), teamId, teamName, projectId,
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