using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _ownerUserName = "OwnerUserName";
    private string _projectName = "ProjectName";

    public ProjectDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectDocumentBuilder WithUserName(string userName)
    {
        _ownerUserName = userName;
        return this;
    }

    public ProjectDocumentBuilder WithProjectName(string projectName)
    {
        _projectName = projectName;
        return this;
    }
    
    
    public ProjectDocument Build()
    {
        return ProjectDocument.Create(_id, _ownerUserName, _projectName);
    }

    public static implicit operator ProjectDocument(ProjectDocumentBuilder builder)
    {
        return builder.Build();
    }
}

public static class GiveMeExtensions
{
    public static ProjectDocumentBuilder Project(this GiveMe _)
    {
        return new ProjectDocumentBuilder();
    }
}