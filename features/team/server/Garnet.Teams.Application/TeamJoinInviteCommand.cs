using FluentResults;
using Garnet.Common.Application;
using Garnet.Teams.Application.Errors;

namespace Garnet.Teams.Application
{
    public class TeamJoinInviteCommand
    {
        public TeamJoinInviteCommand()
        {

        }

        public Task<Result<TeamJoinInvitation>> InviteUserToTeam(CancellationToken ct, ICurrentUserProvider currentUserProvider, TeamJoinInviteArgs args)
        {
            return null;
        }
    }
}