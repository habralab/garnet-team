using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamJoinInvitationRepository : ITeamJoinInvitationRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamJoinInvitationDocument> _f = Builders<TeamJoinInvitationDocument>.Filter;

        public TeamJoinInvitationRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamJoinInvitationEntity> CreateInvitation(CancellationToken ct, string userId, string teamId)
        {
            var db = _dbFactory.Create();
            var invitation = TeamJoinInvitationDocument.Create(Uuid.NewMongo(), userId, teamId);
            await db.TeamJoinInvitations.InsertOneAsync(invitation, cancellationToken: ct);
            return TeamJoinInvitationDocument.ToDomain(invitation);
        }

        public async Task<TeamJoinInvitationEntity[]> FilterInvitations(CancellationToken ct, TeamJoinInvitationFilterArgs filter)
        {
            var db = _dbFactory.Create();

            var userFilter = filter.UserId is null
                ? _f.Empty
                : _f.Eq(x => x.UserId, filter.UserId);

            var teamFilter = filter.TeamId is null
            ? _f.Empty
            : _f.Eq(x => x.TeamId, filter.TeamId);

            var invitations = await db.TeamJoinInvitations
                .Find(userFilter & teamFilter)
                .ToListAsync(ct);

            return invitations.Select(x => TeamJoinInvitationDocument.ToDomain(x)).ToArray();
        }
    }
}