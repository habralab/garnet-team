using System.Security.Claims;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api.TeamCreate;
using Garnet.Teams.Infrastructure.Api.TeamDelete;
using Garnet.Teams.Infrastructure.Api.TeamEditDescription;
using Garnet.Teams.Infrastructure.Api.TeamEditOwner;
using Garnet.Teams.Infrastructure.Api.TeamJoinProjectRequest;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestApprove;
using HotChocolate.Types;
using Garnet.Teams.Application.TeamJoinProjectRequest.Commands;
using Garnet.Teams.Application.TeamJoinInvitation.Commands;
using Garnet.Teams.Application.TeamUserJoinRequest;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamJoinInvitation.Args;
using Garnet.Teams.Application.Team.Commands;
using Garnet.Teams.Application.Team.Args;
using Garnet.Teams.Application.TeamUserJoinRequest.Commands;

namespace Garnet.Teams.Infrastructure.Api
{
    [ExtendObjectType("Mutation")]
    public class TeamsMutation
    {
        private readonly TeamCreateCommand _teamCreateCommand;
        private readonly TeamDeleteCommand _teamDeleteCommand;
        private readonly TeamEditDescriptionCommand _teamEditDescriptionCommand;
        private readonly TeamEditOwnerCommand _teamEditOwnerCommand;
        private readonly TeamUserJoinRequestService _userJoinRequestService;
        private readonly TeamJoinProjectRequestCreateCommand _joinProjectRequestCommand;
        private readonly TeamJoinInviteCommand _joinInviteCommand;
        private readonly TeamUserJoinRequestCreateCommand _teamUserJoinRequestCreateCommand;

        public TeamsMutation(
            TeamCreateCommand teamCreateCommand,
            TeamDeleteCommand teamDeleteCommand,
            TeamEditDescriptionCommand teamEditDescriptionCommand,
            TeamEditOwnerCommand teamEditOwnerCommand,
            TeamUserJoinRequestService userJoinRequestService,
            TeamJoinInviteCommand joinInviteCommand,
            TeamUserJoinRequestCreateCommand teamUserJoinRequestCreateCommand,
            TeamJoinProjectRequestCreateCommand joinProjectRequestCommand)
        {
            _teamCreateCommand = teamCreateCommand;
            _teamDeleteCommand = teamDeleteCommand;
            _teamEditDescriptionCommand = teamEditDescriptionCommand;
            _teamEditOwnerCommand = teamEditOwnerCommand;
            _userJoinRequestService = userJoinRequestService;
            _joinProjectRequestCommand = joinProjectRequestCommand;
            _joinInviteCommand = joinInviteCommand;
            _teamUserJoinRequestCreateCommand = teamUserJoinRequestCreateCommand;
        }

        public async Task<TeamCreatePayload> TeamCreate(CancellationToken ct, ClaimsPrincipal claims, TeamCreateInput input)
        {
            var currentUserProvider = new CurrentUserProvider(claims);
            var args = new TeamCreateArgs(input.Name, input.Description, currentUserProvider.UserId, input.Tags);

            var result = await _teamCreateCommand.Execute(ct, args);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamCreatePayload(team.Id, team.OwnerUserId, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamDeletePayload> TeamDelete(CancellationToken ct, ClaimsPrincipal claims, string teamId)
        {
            var result = await _teamDeleteCommand.Execute(ct, new CurrentUserProvider(claims), teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamDeletePayload(team.Id, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamEditDescriptionPayload> TeamEditDescription(CancellationToken ct, ClaimsPrincipal claims, TeamEditDescriptionInput input)
        {
            var result = await _teamEditDescriptionCommand.Execute(ct, new CurrentUserProvider(claims), input.Id, input.Description);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamEditDescriptionPayload(team.Id, team.Name, team.Description, team.Tags);
        }

        public async Task<TeamEditOwnerPayload> TeamEditOwner(CancellationToken ct, ClaimsPrincipal claims, TeamEditOwnerInput input)
        {
            var result = await _teamEditOwnerCommand.Execute(ct, new CurrentUserProvider(claims), input.TeamId, input.NewOwnerUserId);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamEditOwnerPayload(team.Id, team.Name, team.Description, team.Tags, team.OwnerUserId);
        }

        public async Task<TeamUserJoinRequestPayload> TeamUserJoinRequestCreate(CancellationToken ct, ClaimsPrincipal claims, string teamId)
        {
            var result = await _teamUserJoinRequestCreateCommand.Execute(ct, new CurrentUserProvider(claims), teamId);
            result.ThrowQueryExceptionIfHasErrors();

            var team = result.Value;
            return new TeamUserJoinRequestPayload(team.Id, team.UserId, team.TeamId);
        }

        public async Task<TeamJoinProjectRequestPayload> TeamJoinProjectRequest(CancellationToken ct, ClaimsPrincipal claims, TeamJoinProjectRequestPayload input)
        {
            var result = await _joinProjectRequestCommand.SendJoinProjectRequest(ct, new CurrentUserProvider(claims), input.TeamId, input.ProjectId);
            result.ThrowQueryExceptionIfHasErrors();

            var joinProjectRequest = result.Value;
            return new TeamJoinProjectRequestPayload(joinProjectRequest.Id, joinProjectRequest.TeamId, joinProjectRequest.ProjectId);
        }

        public async Task<TeamJoinInvitePayload> TeamJoinInvite(CancellationToken ct, ClaimsPrincipal claims, TeamJoinInviteInput input)
        {
            var inviteArgs = new TeamJoinInviteArgs(input.UserId, input.TeamId);
            var result = await _joinInviteCommand.InviteUserToTeam(ct, new CurrentUserProvider(claims), inviteArgs);
            result.ThrowQueryExceptionIfHasErrors();

            var invitation = result.Value;
            return new TeamJoinInvitePayload(invitation.Id, invitation.UserId, invitation.TeamId);
        }

        public async Task<TeamUserJoinRequestPayload> TeamUserJoinRequestDecide(CancellationToken ct, ClaimsPrincipal claims, TeamUserJoinRequestDecideInput input)
        {
            var result = await _userJoinRequestService.UserJoinRequestDecide(ct, new CurrentUserProvider(claims), input.UserJoinRequestId, input.IsApproved);
            result.ThrowQueryExceptionIfHasErrors();

            var userJoinRequest = result.Value;
            return new TeamUserJoinRequestPayload(userJoinRequest.Id, userJoinRequest.UserId, userJoinRequest.TeamId);

        }
    }
}