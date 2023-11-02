using Garnet.Teams.Infrastructure.Api.TeamGet;
using Garnet.Teams.Infrastructure.Api.TeamParticipantSearch;

namespace Garnet.Teams.Infrastructure.Api.TeamsListByUser
{
    public record TeamByUserPayload(
       string Id,
       string Name,
       string Description,
       string? AvatarUrl,
       string[] Tags,
       string OwnerUserId,
       int ProjectCount,
       int ParticipantCount,
       string[] TeamParticipantAvatarUrls
   ) : TeamPayload(Id, Name, Description, AvatarUrl, Tags, OwnerUserId);
}