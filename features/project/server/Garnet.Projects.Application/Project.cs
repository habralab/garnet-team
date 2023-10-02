using Garnet.Projects.Events;

namespace Garnet.Projects.Application;

public record Project(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description
);

public static class ProjectDocumentExtensions
{
    public static ProjectCreatedEvent ToUpdatedEvent(this Project doc)
    {
        return new ProjectCreatedEvent(doc.Id, doc.ProjectName);
    }

    public static ProjectUpdatedEvent ToCreatedEvent(this Project doc)
    {
        return new ProjectUpdatedEvent(doc.Id, doc.ProjectName, doc.OwnerUserId, doc.Description);
    }
}