using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb;

public class ProjectsRepository : IProjectsRepository
{
    private readonly DbFactory _dbFactory;
    private readonly FilterDefinitionBuilder<ProjectDocument> _f = Builders<ProjectDocument>.Filter;
    private readonly UpdateDefinitionBuilder<ProjectDocument> _u = Builders<ProjectDocument>.Update;
    private readonly IndexKeysDefinitionBuilder<ProjectDocument> _i = Builders<ProjectDocument>.IndexKeys;

    public ProjectsRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }


    public async Task<Project> CreateProject(CancellationToken ct, string ownerUserId, string projectName,
        string? description)
    {
        var db = _dbFactory.Create();
        var project = ProjectDocument.Create(
            Uuid.NewMongo(),
            ownerUserId,
            projectName,
            description);
        await db.Projects.InsertOneAsync(project, cancellationToken: ct);
        return ProjectDocument.ToDomain(project);
    }

    public async Task<Project?> GetProject(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();
        var project = await db.Projects.Find(o => o.Id == projectId).FirstOrDefaultAsync(ct);
        return project is not null ? ProjectDocument.ToDomain(project) : null;
    }

    public async Task<Project> EditProjectDescription(CancellationToken ct, string projectId, string? description)
    {
        var db = _dbFactory.Create();
        var project = await db.Projects.FindOneAndUpdateAsync(
            _f.Eq(x => x.Id, projectId),
            _u.Set(x => x.Description, description),
            options: new FindOneAndUpdateOptions<ProjectDocument>
            {
                ReturnDocument = ReturnDocument.After
            },
            cancellationToken: ct
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task<Project?> DeleteProject(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();

        var project = await db.Projects.FindOneAndDeleteAsync(
            _f.Eq(x => x.Id, projectId)
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task<Project> EditProjectOwner(CancellationToken ct, string projectId, string newOwnerUserId)
    {
        var db = _dbFactory.Create();
        var project = await db.Projects.FindOneAndUpdateAsync(
            _f.Eq(x => x.Id, projectId),
            _u.Set(x => x.OwnerUserId, newOwnerUserId),
            options: new FindOneAndUpdateOptions<ProjectDocument>
            {
                ReturnDocument = ReturnDocument.After
            },
            cancellationToken: ct
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task CreateIndexes(CancellationToken ct)
    {
        var db = _dbFactory.Create();
        await db.Projects.Indexes.CreateOneAsync(
            new CreateIndexModel<ProjectDocument>(
                _i.Text(o => o.ProjectName)
                    .Text(o => o.Description)
            ),
            cancellationToken: ct);
    }
}