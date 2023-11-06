using Garnet.Common.Application;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.ProjectTask;
using Garnet.Projects.Application.ProjectTask.Args;
using MongoDB.Driver;


namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTask;

public class ProjectTaskRepository : RepositoryBase, IProjectTaskRepository
{
    private readonly DbFactory _dbFactory;

    private readonly FilterDefinitionBuilder<ProjectTaskDocument> _f = Builders<ProjectTaskDocument>.Filter;
    private readonly UpdateDefinitionBuilder<ProjectTaskDocument> _u = Builders<ProjectTaskDocument>.Update;
    private readonly IndexKeysDefinitionBuilder<ProjectTaskDocument> _i = Builders<ProjectTaskDocument>.IndexKeys;

    public ProjectTaskRepository(
        DbFactory dbFactory,
        ICurrentUserProvider currentUserProvider,
        IDateTimeService dateTimeService) : base(currentUserProvider, dateTimeService)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectTaskEntity> CreateProjectTask(CancellationToken ct, string responsibleUserId,
        string status, int taskNumber, ProjectTaskCreateArgs args)
    {
        var db = _dbFactory.Create();
        var task = ProjectTaskDocument.Create(
            Uuid.NewMongo(),
            taskNumber,
            args.ProjectId,
            responsibleUserId,
            args.Name,
            args.Description,
            status,
            args.TeamExecutorIds,
            args.UserExecutorIds,
            args.Tags,
            args.Labels);

        task = await InsertOneDocument(
            ct,
            db.ProjectTasks,
            task);

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity?> GetProjectTaskById(CancellationToken ct, string taskId)
    {
        var db = _dbFactory.Create();
        var task = await db.ProjectTasks.Find(x => x.Id == taskId).FirstOrDefaultAsync(ct);
        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> DeleteProjectTask(CancellationToken ct, string taskId)
    {
        var db = _dbFactory.Create();
        var task = await db.ProjectTasks.FindOneAndDeleteAsync(
            _f.Eq(x => x.Id, taskId),
            cancellationToken: ct);

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> EditProjectTaskName(CancellationToken ct, string taskId, string newTaskName)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, taskId);
        var update = _u.Set(x => x.Name, newTaskName);

        var task = await FindOneAndUpdateDocument(
            ct,
            db.ProjectTasks,
            filter,
            update
        );

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> EditProjectTaskResponsibleUser(CancellationToken ct, string taskId,
        string newResponsibleUserId)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, taskId);
        var update = _u.Set(x => x.ResponsibleUserId, newResponsibleUserId);

        var task = await FindOneAndUpdateDocument(
            ct,
            db.ProjectTasks,
            filter,
            update
        );

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> EditProjectTaskDescription(CancellationToken ct, string taskId,
        string description)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, taskId);
        var update = _u.Set(x => x.Description, description);

        var task = await FindOneAndUpdateDocument(
            ct,
            db.ProjectTasks,
            filter,
            update
        );

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> EditProjectTaskTags(CancellationToken ct, string taskId, string[] tags)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, taskId);
        var update = _u.Set(x => x.Tags, tags);

        var task = await FindOneAndUpdateDocument(
            ct,
            db.ProjectTasks,
            filter,
            update
        );

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> EditProjectTaskLabels(CancellationToken ct, string taskId, string[] labels)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, taskId);
        var update = _u.Set(x => x.Labels, labels);

        var task = await FindOneAndUpdateDocument(
            ct,
            db.ProjectTasks,
            filter,
            update
        );

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> EditProjectTeamExecutor(CancellationToken ct, string taskId, string[] teamIds)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, taskId);
        var update = _u.Set(x => x.TeamExecutorIds, teamIds);

        var task = await FindOneAndUpdateDocument(
            ct,
            db.ProjectTasks,
            filter,
            update
        );

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> CloseProjectTask(CancellationToken ct, string taskId, string status)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, taskId);
        var update = _u.Set(x => x.Status, status);

        var task = await FindOneAndUpdateDocument(
            ct,
            db.ProjectTasks,
            filter,
            update
        );

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task<ProjectTaskEntity> OpenProjectTask(CancellationToken ct, string taskId, string status)
    {
        var db = _dbFactory.Create();
        var filter = _f.Eq(x => x.Id, taskId);
        var update = _u.Set(x => x.Status, status);

        var task = await FindOneAndUpdateDocument(
            ct,
            db.ProjectTasks,
            filter,
            update
        );

        return ProjectTaskDocument.ToDomain(task);
    }

    public async Task CreateIndexes(CancellationToken ct)
    {
        var db = _dbFactory.Create();
        await db.ProjectTasks.Indexes.CreateOneAsync(
            new CreateIndexModel<ProjectTaskDocument>(
                _i.Text(o => o.Name)
                    .Text(o => o.Description)
                    .Text(o => o.TaskNumber)
            ),
            cancellationToken: ct);
    }
}