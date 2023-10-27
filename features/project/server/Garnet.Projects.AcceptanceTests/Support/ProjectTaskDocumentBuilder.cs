using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectTaskDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _projectId = Uuid.NewMongo();
    private string _userCreatorId = Uuid.NewMongo();
    private string _name = "TaskName";
    private string? _description = "Description";
    private string _status = "Status";
    private string? _teamExecutorId = "TeamExecutorId";
    private string[] _userExecutorIds = Array.Empty<string>();
    private string[] _tags = Array.Empty<string>();
    private string[] _labels = Array.Empty<string>();


    public ProjectTaskDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectTaskDocumentBuilder WithProjectId(string projectId)
    {
        _projectId = projectId;
        return this;
    }

    public ProjectTaskDocumentBuilder WithUserCreatorId(string userCreatorId)
    {
        _userCreatorId = userCreatorId;
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

    public ProjectTaskDocumentBuilder WithTeamExecutorId(string? teamExecutorId)
    {
        _teamExecutorId = teamExecutorId;
        return this;
    }

    public ProjectTaskDocumentBuilder WithUserExecutorId(string[] userExecutorIds)
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

    public ProjectTaskDocument Build()
    {
        return ProjectTaskDocument.Create(_id, _projectId, _userCreatorId, _name, _description, _status,
            _teamExecutorId, _userExecutorIds, _tags, _labels);
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