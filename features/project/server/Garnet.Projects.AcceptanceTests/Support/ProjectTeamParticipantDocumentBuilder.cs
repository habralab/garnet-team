using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectTeamParticipantDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _teamId = Uuid.NewMongo();
    private string _teamName = "TeamName";
    private string _projectId = Uuid.NewMongo();
    private string _teamAvatarUrl = "";

    private List<ProjectUserDocument> _userParticipants = new List<ProjectUserDocument>()
    {
        new ProjectUserDocument()
    };

    private List<ProjectDocument> _projects = new List<ProjectDocument>()
    {
        new ProjectDocument()
    };


    public ProjectTeamParticipantDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectTeamParticipantDocumentBuilder WithTeamId(string teamId)
    {
        _teamId = teamId;
        return this;
    }

    public ProjectTeamParticipantDocumentBuilder WithTeamName(string teamName)
    {
        _teamName = teamName;
        return this;
    }

    public ProjectTeamParticipantDocumentBuilder WithProjectId(string projectId)
    {
        _projectId = projectId;
        return this;
    }

    public ProjectTeamParticipantDocument Build()
    {
        return ProjectTeamParticipantDocument.Create(
            _id,
            _teamId,
            _teamName,
            _projectId,
            _teamAvatarUrl,
            _userParticipants.ToArray(),
            _projects.ToArray());
    }

    public static implicit operator ProjectTeamParticipantDocument(
        ProjectTeamParticipantDocumentBuilder documentBuilder)
    {
        return documentBuilder.Build();
    }
}

public static partial class GiveMeExtensions
{
    public static ProjectTeamParticipantDocumentBuilder ProjectTeamParticipant(this GiveMe _)
    {
        return new ProjectTeamParticipantDocumentBuilder();
    }
}