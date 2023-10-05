using Garnet.Projects.Events;

namespace Garnet.Projects.Application;

public record Project(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description,
    string[] Tags
);

public static class ProjectDocumentExtensions
{
    public static ProjectUpdatedEvent ToUpdatedEvent(this Project doc)
    {
        return new ProjectUpdatedEvent(doc.Id, doc.ProjectName, doc.OwnerUserId, doc.Description);
    }

    public static ProjectCreatedEvent ToCreatedEvent(this Project doc)
    {
        return new ProjectCreatedEvent(doc.Id, doc.ProjectName, doc.OwnerUserId, doc.Description);
    }

    public static ProjectDeletedEvent ToDeletedEvent(this Project doc)
    {
        return new ProjectDeletedEvent(doc.Id, doc.ProjectName, doc.OwnerUserId, doc.Description);
    }
}