using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectTaskDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private int _taskNumber = 0;
    private string _projectId = Uuid.NewMongo();
    private string _responsibleUserId = Uuid.NewMongo();
    private string _name = "TaskName";
    private string? _description = "Description";
    private string _status = "Status";
    private string[] _teamExecutorIds = Array.Empty<string>();
    private string[] _userExecutorIds = Array.Empty<string>();
    private string[] _tags = Array.Empty<string>();
    private string[] _labels = Array.Empty<string>();
    private AuditInfoDocument _auditInfo = new(DateTime.UtcNow, "CreatedByUser", DateTime.UtcNow, "UpdatedByUser", 0);


    public ProjectTaskDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectTaskDocumentBuilder WithTaskNumber(int taskNumber)
    {
        _taskNumber = taskNumber;
        return this;
    }

    public ProjectTaskDocumentBuilder WithProjectId(string projectId)
    {
        _projectId = projectId;
        return this;
    }

    public ProjectTaskDocumentBuilder WithResponsibleUserId(string responsibleUserId)
    {
        _responsibleUserId = responsibleUserId;
        return this;
    }

    public ProjectTaskDocumentBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProjectTaskDocumentBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public ProjectTaskDocumentBuilder WithStatus(string status)
    {
        _status = status;
        return this;
    }

    public ProjectTaskDocumentBuilder WithTeamExecutorIds(string[] teamExecutorIds)
    {
        _teamExecutorIds = teamExecutorIds;
        return this;
    }

    public ProjectTaskDocumentBuilder WithUserExecutorIds(string[] userExecutorIds)
    {
        _userExecutorIds = userExecutorIds;
        return this;
    }

    public ProjectTaskDocumentBuilder WithTags(string[] tags)
    {
        _tags = tags;
        return this;
    }

    public ProjectTaskDocumentBuilder WithLabels(string[] labels)
    {
        _labels = labels;
        return this;
    }

    public ProjectTaskDocumentBuilder WithCreatedBy(string username)
    {
        _auditInfo = new AuditInfoDocument(DateTime.UtcNow, username, DateTime.UtcNow, username, 0);
        return this;
    }

    public ProjectTaskDocument Build()
    {
        return ProjectTaskDocument.Create(_id, _taskNumber, _projectId, _responsibleUserId, _name, _description, _status,
                _teamExecutorIds, _userExecutorIds, _tags, _labels)
            with
            {
                AuditInfo = _auditInfo
            };
        ;
    }

    public static implicit operator ProjectTaskDocument(ProjectTaskDocumentBuilder builder)
    {
        return builder.Build();
    }
}

public static partial class GiveMeExtensions
{
    public static ProjectTaskDocumentBuilder ProjectTask(this GiveMe _)
    {
        return new ProjectTaskDocumentBuilder();
    }
}