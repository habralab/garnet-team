using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api.ProjectCreate;

public record ProjectCreateInput(
    string ProjectName,
    string Description,
    IFile? File,
    string[] Tags);