using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Args;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.Project;

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


    public async Task<ProjectEntity> CreateProject(CancellationToken ct, string ownerUserId, ProjectCreateArgs args)
    {
        var db = _dbFactory.Create();
        var project = ProjectDocument.Create(
            Uuid.NewMongo(),
            ownerUserId,
            args.ProjectName,
            args.Description,
            args.AvatarUrl,
            args.Tags);
        await db.Projects.InsertOneAsync(project, cancellationToken: ct);
        return ProjectDocument.ToDomain(project);
    }

    public async Task<ProjectEntity?> GetProject(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();
        var project = await db.Projects.Find(o => o.Id == projectId).FirstOrDefaultAsync(ct);
        return project is not null ? ProjectDocument.ToDomain(project) : null;
    }

    public async Task<ProjectEntity[]> FilterProjects(CancellationToken ct, ProjectFilterArgs args)
    {
        var db = _dbFactory.Create();

        var search = args.Search?.ToLower();
        var searchFilter = search is null
            ? _f.Empty
            : _f.Where(x =>
                (x.Description != null && x.Description.ToLower().Contains(search)) ||
                x.ProjectName.ToLower().Contains(search));

        var tagsFilter = args.Tags.Length > 0
            ? _f.All(o => o.Tags, args.Tags)
            : _f.Empty;

        var projects = await db.Projects
            .Find(searchFilter & tagsFilter)
            .Skip(args.Skip)
            .Limit(args.Take)
            .ToListAsync(ct);

        return projects.Select(ProjectDocument.ToDomain).ToArray();
    }

    public async Task<ProjectEntity> EditProjectDescription(CancellationToken ct, string projectId, string? description)
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

    public async Task<ProjectEntity> EditProjectName(CancellationToken ct, string projectId, string newName)
    {
        var db = _dbFactory.Create();
        var project = await db.Projects.FindOneAndUpdateAsync(
            _f.Eq(x => x.Id, projectId),
            _u.Set(x => x.ProjectName, newName),
            options: new FindOneAndUpdateOptions<ProjectDocument>
            {
                ReturnDocument = ReturnDocument.After
            },
            cancellationToken: ct
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task<ProjectEntity> EditProjectAvatar(CancellationToken ct, string projectId, string avatarUrl)
    {
        var db = _dbFactory.Create();
        var project = await db.Projects.FindOneAndUpdateAsync(
            _f.Eq(x => x.Id, projectId),
            _u.Set(x => x.AvatarUrl, avatarUrl),
            options: new FindOneAndUpdateOptions<ProjectDocument>
            {
                ReturnDocument = ReturnDocument.After
            },
            cancellationToken: ct
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task<ProjectEntity?> DeleteProject(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();

        var project = await db.Projects.FindOneAndDeleteAsync(
            _f.Eq(x => x.Id, projectId)
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task<ProjectEntity> EditProjectOwner(CancellationToken ct, string projectId, string newOwnerUserId)
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