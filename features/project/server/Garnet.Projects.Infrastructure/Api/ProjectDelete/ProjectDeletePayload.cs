using Garnet.Projects.Infrastructure.Api.ProjectGet;

namespace Garnet.Projects.Infrastructure.Api.ProjectDelete;

public record ProjectDeletePayload(string Id, string OwnerUserId, string ProjectName, string? Description);