using Garnet.Projects.Events.Project;

namespace Garnet.Projects.Application.Project;

public record ProjectEntity(
    string Id,
    string OwnerUserId,
    string ProjectName,
    string? Description,
    string? AvatarUrl,
    string[] Tags
);

public static class ProjectDocumentExtensions
{
    public static ProjectUpdatedEvent ToUpdatedEvent(this ProjectEntity doc)
    {
        return new ProjectUpdatedEvent(doc.Id, doc.ProjectName, doc.OwnerUserId, doc.Description, doc.AvatarUrl, doc.Tags);
    }

    public static ProjectCreatedEvent ToCreatedEvent(this ProjectEntity doc)
    {
        return new ProjectCreatedEvent(doc.Id, doc.ProjectName, doc.OwnerUserId, doc.Description, doc.AvatarUrl, doc.Tags);
    }

    public static ProjectDeletedEvent ToDeletedEvent(this ProjectEntity doc)
    {
        return new ProjectDeletedEvent(doc.Id, doc.ProjectName, doc.OwnerUserId, doc.Description, doc.AvatarUrl, doc.Tags);
    }
}