using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectTeamParticipantBuilder
{
    private string _id = Uuid.NewMongo();
    private string _teamId = Uuid.NewMongo();
    private string _projectId = Uuid.NewMongo();


    public ProjectTeamParticipantBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectTeamParticipantBuilder WithTeamId(string teamId)
    {
        _teamId = teamId;
        return this;
    }

    public ProjectTeamParticipantBuilder WithProjectId(string projectId)
    {
        _projectId = projectId;
        return this;
    }

    public ProjectTeamParticipantDocument Build()
    {
        return ProjectTeamParticipantDocument.Create(_id, _teamId, _projectId);
    }

    public static implicit operator ProjectTeamParticipantDocument(ProjectTeamParticipantBuilder builder)
    {
        return builder.Build();
    }
}


public static partial class GiveMeExtensions
{
    public static ProjectDocumentBuilder ProjectTeamParticipant(this GiveMe _)
    {
        return new ProjectDocumentBuilder();
    }
}