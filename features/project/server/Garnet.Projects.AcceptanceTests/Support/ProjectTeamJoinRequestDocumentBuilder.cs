using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectTeamJoinRequestDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _teamId = Uuid.NewMongo();
    private string _teamName = "TeamName";
    private string _projectId = Uuid.NewMongo();


    public ProjectTeamJoinRequestDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectTeamJoinRequestDocumentBuilder WithTeamId(string teamId)
    {
        _teamId = teamId;
        return this;
    }
    public ProjectTeamJoinRequestDocumentBuilder WithTeamName(string teamName)
    {
        _teamName = teamName;
        return this;
    }


    public ProjectTeamJoinRequestDocumentBuilder WithProjectId(string projectId)
    {
        _projectId = projectId;
        return this;
    }

    public ProjectTeamJoinRequestDocument Build()
    {
        return ProjectTeamJoinRequestDocument.Create(_id, _teamId, _teamName, _projectId);
    }

    public static implicit operator ProjectTeamJoinRequestDocument(ProjectTeamJoinRequestDocumentBuilder documentBuilder)
    {
        return documentBuilder.Build();
    }
}


public static partial class GiveMeExtensions
{
    public static ProjectTeamJoinRequestDocumentBuilder ProjectTeamJoinRequest(this GiveMe _)
    {
        return new ProjectTeamJoinRequestDocumentBuilder();
    }
}