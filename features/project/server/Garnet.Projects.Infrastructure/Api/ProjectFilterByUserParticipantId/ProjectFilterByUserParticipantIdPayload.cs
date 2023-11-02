using Garnet.Projects.Infrastructure.Api.ProjectCreate;

namespace Garnet.Projects.Infrastructure.Api.ProjectFilterByUserParticipantId;

public record ProjectFilterByUserParticipantIdPayload(ProjectCreatePayload[] Projects);
