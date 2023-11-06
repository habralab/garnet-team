using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application.TeamProject;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamProject
{
    public class TeamProjectRepository : ITeamProjectRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamProjectDocument> _f = Builders<TeamProjectDocument>.Filter;

        public TeamProjectRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamProjectEntity> AddTeamProject(CancellationToken ct, string projectId, string teamId)
        {
            var db = _dbFactory.Create();
            var project = TeamProjectDocument.Create(Uuid.NewMongo(), teamId, projectId);
            await db.TeamProjects.InsertOneAsync(project, cancellationToken: ct);
            return TeamProjectDocument.ToDomain(project);
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

        public async Task<TeamProjectEntity[]> GetTeamProjectByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var projects = await db.TeamProjects.Find(
                _f.Eq(x => x.TeamId, teamId)
            ).ToListAsync(ct);

            return projects.Select(x => TeamProjectDocument.ToDomain(x)).ToArray();
        }

        public async Task<TeamProjectEntity?> RemoveTeamProjectInTeam(CancellationToken ct, string projectId, string teamId)
        {
            var db = _dbFactory.Create();
            var project = await db.TeamProjects.FindOneAndDeleteAsync(
                _f.And(
                    _f.Eq(x => x.TeamId, teamId),
                    _f.Eq(x => x.ProjectId, projectId)
                ),
                cancellationToken: ct
            );

            return project is null ? null : TeamProjectDocument.ToDomain(project);
        }
    }
}