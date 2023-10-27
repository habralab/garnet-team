using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb.Project;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _ownerUserId = "OwnerUserId";
    private string _projectName = "ProjectName";
    private string? _description = "Description";
    private string? _avatarUrl = "AvatarUrl";
    private string[] _tags = Array.Empty<string>();
    private int _tasksCounter = 0;
    public AuditInfoDocument? _auditInfo;

    public ProjectDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectDocumentBuilder WithOwnerUserId(string userId)
    {
        _ownerUserId = userId;
        return this;
    }

    public ProjectDocumentBuilder WithProjectName(string projectName)
    {
        _projectName = projectName;
        return this;
    }

    public ProjectDocumentBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public ProjectDocumentBuilder WithAvatarUrl(string? avatarUrl)
    {
        _avatarUrl = avatarUrl;
        return this;
    }

    public ProjectDocumentBuilder WithTags(string[] tags)
    {
        _tags = tags;
        return this;
    }

    public ProjectDocumentBuilder WithTasksCounter(int tasksCounter)
    {
        _tasksCounter = tasksCounter;
        return this;
    }

    public ProjectDocumentBuilder WithAuditInfo(AuditInfoDocument auditInfo)
    {
        _auditInfo = auditInfo;
        return this;
    }


    public ProjectDocument Build()
    {
        return ProjectDocument.Create(_id, _ownerUserId, _projectName, _description, _avatarUrl, _tags, _tasksCounter);
    }

    public static implicit operator ProjectDocument(ProjectDocumentBuilder builder)
    {
        return builder.Build();
    }
}

public static partial class GiveMeExtensions
{
    public static ProjectDocumentBuilder Project(this GiveMe _)
    {
        return new ProjectDocumentBuilder();
    }
}