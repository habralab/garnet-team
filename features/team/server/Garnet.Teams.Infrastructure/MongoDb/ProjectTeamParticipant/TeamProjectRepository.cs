using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application.ProjectTeamParticipant;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb.ProjectTeamParticipant
{
    public class TeamProjectRepository : ITeamProjectRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<ProjectTeamParticipantDocument> _f = Builders<ProjectTeamParticipantDocument>.Filter;

        public TeamProjectRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<ProjectTeamParticipantEntity> AddTeamProject(CancellationToken ct, string projectId, string teamId)
        {
            var db = _dbFactory.Create();
            var project = ProjectTeamParticipantDocument.Create(Uuid.NewMongo(), teamId, projectId);
            await db.TeamProjects.InsertOneAsync(project, cancellationToken: ct);
            return ProjectTeamParticipantDocument.ToDomain(project);
        }

        public async Task DeleteAllTeamProjectByProject(CancellationToken ct, string projectId)
        {
            var db = _dbFactory.Create();
            await db.TeamProjects.DeleteManyAsync(
                _f.Eq(x => x.ProjectId, projectId),
                ct
            );
        }

        public async Task DeleteAllTeamProjectByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            await db.TeamProjects.DeleteManyAsync(
                _f.Eq(x => x.TeamId, teamId),
                ct
            );
        }

        public async Task<ProjectTeamParticipantEntity?> RemoveTeamProjectInTeam(CancellationToken ct, string projectId, string teamId)
        {
            var db = _dbFactory.Create();
            var project = await db.TeamProjects.FindOneAndDeleteAsync(
                _f.And(
                    _f.Eq(x => x.TeamId, teamId),
                    _f.Eq(x => x.ProjectId, projectId)
                ),
                cancellationToken: ct
            );

            return project is null ? null : ProjectTeamParticipantDocument.ToDomain(project);
        }

        public async Task<ProjectTeamParticipantEntity[]> TeamProjectListOfTeams(CancellationToken ct, string[] teamIds)
        {
            var db = _dbFactory.Create();
            var projects = await db.TeamProjects.Find(
                _f.In(x => x.TeamId, teamIds)
            ).ToListAsync(ct);

            return projects.Select(x => ProjectTeamParticipantDocument.ToDomain(x)).ToArray();
        }
    }
}