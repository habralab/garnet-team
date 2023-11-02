using Garnet.Projects.Infrastructure.Api.ProjectCreate;

namespace Garnet.Projects.Infrastructure.Api.ProjectFilterByTeamParticipantId;

public record ProjectFilterByTeamParticipantIdPayload(ProjectCreatePayload[] Projects);
