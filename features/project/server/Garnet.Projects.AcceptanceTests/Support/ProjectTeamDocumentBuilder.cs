using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeam;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectTeamDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _teamName = "TeamName";
    private string _ownerUserId = Uuid.NewMongo();


    public ProjectTeamDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectTeamDocumentBuilder WithTeamName(string teamName)
    {
        _teamName = teamName;
        return this;
    }

    public ProjectTeamDocumentBuilder WithOwnerUserId(string ownerUserId)
    {
        _ownerUserId = ownerUserId;
        return this;
    }

    public ProjectTeamDocument Build()
    {
        return ProjectTeamDocument.Create(_id, _teamName, _ownerUserId);
    }

    public static implicit operator ProjectTeamDocument(ProjectTeamDocumentBuilder documentBuilder)
    {
        return documentBuilder.Build();
    }
}


public static partial class GiveMeExtensions
{
    public static ProjectTeamDocumentBuilder ProjectTeam(this GiveMe _)
    {
        return new ProjectTeamDocumentBuilder();
    }
}