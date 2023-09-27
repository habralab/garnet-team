namespace Garnet.Projects.Application;

public record Project(
    string Id,
    string OwnerUserId,
    string ProjectName
);