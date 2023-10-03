using FluentResults;
using Garnet.Common.Application;

namespace Garnet.Teams.Application
{
    public class TeamMembershipService
    {
        public TeamMembershipService()
        {

        }

        public Task<Result<TeamUserJoinRequest>> JoinRequestByUser(CancellationToken ct, string teamId, ICurrentUserProvider currentUserProvider)
        {
            return null;
        }
    }
}