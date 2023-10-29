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