using Garnet.Common.Application;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application.TeamJoinInvitation;
using Garnet.Teams.Application.TeamJoinInvitation.Args;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamJoinInvitation
{
    public class TeamJoinInvitationRepository : RepositoryBase, ITeamJoinInvitationRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamJoinInvitationDocument> _f = Builders<TeamJoinInvitationDocument>.Filter;

        public TeamJoinInvitationRepository(
            ICurrentUserProvider currentUserProvider,
            IDateTimeService dateTimeService,
            DbFactory dbFactory) : base(currentUserProvider, dateTimeService)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamJoinInvitationEntity> CreateInvitation(CancellationToken ct, string userId, string teamId)
        {
            var db = _dbFactory.Create();
            var invitation = TeamJoinInvitationDocument.Create(Uuid.NewMongo(), userId, teamId);

            invitation = await InsertOneDocument(
                ct,
                db.TeamJoinInvitations,
                invitation
            );

            return TeamJoinInvitationDocument.ToDomain(invitation);
        }

        public async Task<TeamJoinInvitationEntity?> DeleteInvitationById(CancellationToken ct, string joinInvitationId)
        {
            var db = _dbFactory.Create();

            var invitation = await db.TeamJoinInvitations.FindOneAndDeleteAsync(
                _f.Eq(x => x.Id, joinInvitationId)
            );

            return invitation is null ? null : TeamJoinInvitationDocument.ToDomain(invitation);
        }

        public async Task DeleteInvitationsByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            await db.TeamJoinInvitations.DeleteManyAsync(
                _f.Eq(x => x.TeamId, teamId),
                cancellationToken: ct
            );
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

        public async Task<TeamJoinInvitationEntity?> GetById(CancellationToken ct, string joinInvitationId)
        {
            var db = _dbFactory.Create();

            var invitation = await db.TeamJoinInvitations.Find(
                _f.Eq(x => x.Id, joinInvitationId)
            ).FirstOrDefaultAsync(ct);

            return invitation is null ? null : TeamJoinInvitationDocument.ToDomain(invitation);
        }
    }
}