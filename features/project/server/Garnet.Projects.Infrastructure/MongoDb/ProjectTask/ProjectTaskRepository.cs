using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.ProjectTask;
using Garnet.Projects.Application.ProjectTask.Args;


namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTask;

public class ProjectTaskRepository : IProjectTaskRepository
{
    private readonly DbFactory _dbFactory;

    public ProjectTaskRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectTaskEntity> CreateProjectTask(CancellationToken ct,string userCreatorId, ProjectTaskCreateArgs args)
    {
        var db = _dbFactory.Create();
        var task = ProjectTaskDocument.Create(
            Uuid.NewMongo(),
            args.ProjectId,
            userCreatorId,
            args.Name,
            args.Description,
            args.Status,
            args.TeamExecutorId,
            args.UserExecutorId,
            args.Tags);
        await db.ProjectTasks.InsertOneAsync(task, cancellationToken: ct);
        return ProjectTaskDocument.ToDomain(task);
    }
}