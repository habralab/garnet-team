using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _ownerUserId = "OwnerUserId";
    private string _projectName = "ProjectName";

    public ProjectDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectDocumentBuilder WithUserName(string userId)
    {
        _ownerUserId = userId;
        return this;
    }

    public ProjectDocumentBuilder WithProjectName(string projectName)
    {
        _projectName = projectName;
        return this;
    }
    
    
    public ProjectDocument Build()
    {
        return ProjectDocument.Create(_id, _ownerUserId, _projectName);
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