using Garnet.Common.Application;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.ProjectTask;
using Garnet.Projects.Application.ProjectTask.Args;


namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTask;

public class ProjectTaskRepository : RepositoryBase, IProjectTaskRepository
{
    private readonly DbFactory _dbFactory;

    public ProjectTaskRepository(
        DbFactory dbFactory,
        ICurrentUserProvider currentUserProvider,
        IDateTimeService dateTimeService) : base(currentUserProvider, dateTimeService)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectTaskEntity> CreateProjectTask(CancellationToken ct, string userCreatorId,
        string status, int taskNumber, ProjectTaskCreateArgs args)
    {
        var db = _dbFactory.Create();
        var task = ProjectTaskDocument.Create(
            Uuid.NewMongo(),
            taskNumber,
            args.ProjectId,
            userCreatorId,
            args.Name,
            args.Description,
            status,
            args.TeamExecutorId,
            args.UserExecutorIds,
            args.Tags,
            args.Labels);

        task = await InsertOneDocument(
            ct,
            db.ProjectTasks,
            task);

        return ProjectTaskDocument.ToDomain(task);
    }
}