using Garnet.Common.Application;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Args;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.Project;

public class ProjectRepository : RepositoryBase, IProjectRepository
{
    private readonly DbFactory _dbFactory;
    private readonly FilterDefinitionBuilder<ProjectDocument> _f = Builders<ProjectDocument>.Filter;
    private readonly UpdateDefinitionBuilder<ProjectDocument> _u = Builders<ProjectDocument>.Update;
    private readonly IndexKeysDefinitionBuilder<ProjectDocument> _i = Builders<ProjectDocument>.IndexKeys;

    public ProjectRepository(
        DbFactory dbFactory,
        ICurrentUserProvider currentUserProvider,
        IDateTimeService dateTimeService) : base(currentUserProvider, dateTimeService)
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
            null,
            args.Tags,
            0);

        project = await InsertOneDocument(
            ct,
            db.Projects,
            project
        );
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
        var filter = _f.Eq(x => x.Id, projectId);
        var update = _u.Set(x => x.Description, description);

        var project = await FindOneAndUpdateDocument(
            ct,
            db.Projects,
            filter,
            update
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task<ProjectEntity> EditProjectName(CancellationToken ct, string projectId, string newName)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, projectId);
        var update = _u.Set(x => x.ProjectName, newName);

        var project = await FindOneAndUpdateDocument(
            ct,
            db.Projects,
            filter,
            update
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task<ProjectEntity> EditProjectAvatar(CancellationToken ct, string projectId, string avatarUrl)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, projectId);
        var update = _u.Set(x => x.AvatarUrl, avatarUrl);

        var project = await FindOneAndUpdateDocument(
            ct,
            db.Projects,
            filter,
            update
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
        var filter = _f.Eq(x => x.Id, projectId);
        var update = _u.Set(x => x.OwnerUserId, newOwnerUserId);

        var project = await FindOneAndUpdateDocument(
            ct,
            db.Projects,
            filter,
            update
        );

        return ProjectDocument.ToDomain(project);
    }

    public async Task IncrementProjectTasksCounter(CancellationToken ct, string projectId)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, projectId);
        var update = _u.Inc(x => x.TasksCounter, 1);

        await FindOneAndUpdateDocument(
            ct,
            db.Projects,
            filter,
            update
        );
    }

    public async Task CreateIndexes(CancellationToken ct)
    {
        var db = _dbFactory.Create();
        await db.Projects.Indexes.CreateOneAsync(
            new CreateIndexModel<ProjectDocument>(
                _i.Text(o => o.ProjectName)
                    .Text(o => o.Description)
                    .Text(o => o.Tags)
            ),
            cancellationToken: ct);
    }
}