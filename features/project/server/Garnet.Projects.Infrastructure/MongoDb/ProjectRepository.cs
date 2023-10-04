using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb;

public class ProjectRepository : IProjectRepository
{
    private readonly DbFactory _dbFactory;
    private readonly FilterDefinitionBuilder<ProjectDocument> _f = Builders<ProjectDocument>.Filter;
    private readonly UpdateDefinitionBuilder<ProjectDocument> _u = Builders<ProjectDocument>.Update;
    private readonly IndexKeysDefinitionBuilder<ProjectDocument> _i = Builders<ProjectDocument>.IndexKeys;

    public ProjectRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }


    public async Task<Project> CreateProject(CancellationToken ct, string ownerUserId, string projectName,
        string? description, string[] tags)
    {
        var db = _dbFactory.Create();
        var project = ProjectDocument.Create(
            Uuid.NewMongo(),
            ownerUserId,
            projectName,
            description,
            tags);
        await db.Projects.InsertOneAsync(project, cancellationToken: ct);
        return ProjectDocument.ToDomain(project);
    }

    public async Task<Project?> GetProject(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();
        var project = await db.Projects.Find(o => o.Id == projectId).FirstOrDefaultAsync(ct);
        return project is not null ? ProjectDocument.ToDomain(project) : null;
    }

    public async Task<Project[]> FilterProjects(CancellationToken ct, string? search, string[] tags, int skip, int take)
    {
        var db = _dbFactory.Create();

        search = search?.ToLower();
        var searchFilter = search is null
            ? _f.Empty
            : _f.Where(x =>
                (x.Description != null && x.Description.ToLower().Contains(search)) ||
                x.ProjectName.ToLower().Contains(search));

        var tagsFilter = tags.Length > 0
            ? _f.All(o => o.Tags, tags)
            : _f.Empty;

        var projects = await db.Projects
            .Find(searchFilter & tagsFilter)
            .Skip(skip)
            .Limit(take)
            .ToListAsync(ct);

        return projects.Select(ProjectDocument.ToDomain).ToArray();
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