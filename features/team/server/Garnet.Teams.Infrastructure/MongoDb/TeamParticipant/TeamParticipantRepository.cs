using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamParticipant.Args;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamParticipant
{
    public class TeamParticipantRepository : ITeamParticipantRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamParticipantDocument> _f = Builders<TeamParticipantDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamParticipantDocument> _u = Builders<TeamParticipantDocument>.Update;
        private readonly IndexKeysDefinitionBuilder<TeamParticipantDocument> _i = Builders<TeamParticipantDocument>.IndexKeys;

        public TeamParticipantRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamParticipantEntity[]> GetParticipantsFromTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var teamParticipants = await db.TeamParticipants.Find(x => x.TeamId == teamId).ToListAsync();
            return teamParticipants.Select(o => TeamParticipantDocument.ToDomain(o)).ToArray();
        }

        public async Task<TeamParticipantEntity[]> GetMembershipOfUser(CancellationToken ct, string userId)
        {
            var db = _dbFactory.Create();
            var userTeams = await db.TeamParticipants.Find(
                _f.Eq(x => x.UserId, userId)
            ).ToListAsync();

            return userTeams.Select(x => TeamParticipantDocument.ToDomain(x)).ToArray();
        }

        public async Task<TeamParticipantEntity[]> FilterTeamParticipants(CancellationToken ct, TeamParticipantFilterArgs filter)
        {
            var db = _dbFactory.Create();

            var searchFilter = filter.Search is null
                ? _f.Empty
                : _f.Where(x => x.Username.ToLower().Contains(filter.Search.ToLower()));

            var teamFilter = filter.TeamId is null
                ? _f.Empty
                : _f.Eq(x => x.TeamId, filter.TeamId);

            var participants = await db.TeamParticipants
                .Find(searchFilter & teamFilter)
                .Skip(filter.Skip)
                .Limit(filter.Take)
                .ToListAsync(ct);

            return participants.Select(x => TeamParticipantDocument.ToDomain(x)).ToArray();
        }

        public async Task UpdateTeamParticipant(CancellationToken ct, string userId, TeamParticipantUpdateArgs update)
        {
            var db = _dbFactory.Create();
            await db.TeamParticipants.UpdateManyAsync(
                _f.Eq(x => x.UserId, userId),
                _u
                    .Set(x => x.Username, update.Username)
                    .Set(x => x.AvatarUrl, update.AvatarUrl),
                cancellationToken: ct
            );
        }

        public async Task CreateIndexes(CancellationToken ct)
        {
            var db = _dbFactory.Create();
            await db.TeamParticipants.Indexes.CreateOneAsync(
                new CreateIndexModel<TeamParticipantDocument>(
                    _i.Text(o => o.Username)
                ),
                cancellationToken: ct);
        }

        public async Task DeleteParticipantsByTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            await db.TeamParticipants.DeleteManyAsync(
                _f.Eq(x => x.TeamId, teamId),
                cancellationToken: ct
            );
        }

        public async Task DeleteParticipantById(CancellationToken ct, string participantId)
        {
            var db = _dbFactory.Create();
            await db.TeamParticipants.DeleteOneAsync(
                _f.Eq(x => x.Id, participantId)
            );
        }

        public async Task<TeamParticipantEntity?> IsParticipantInTeam(CancellationToken ct, string userId, string teamId)
        {
            var db = _dbFactory.Create();

            var membership = await db.TeamParticipants.Find(
                _f.Eq(x => x.TeamId, teamId)
                & _f.Eq(x => x.UserId, userId)
            ).FirstOrDefaultAsync(ct);

            return membership is null ? null : TeamParticipantDocument.ToDomain(membership);
        }

        public async Task<TeamParticipantEntity> CreateTeamParticipant(CancellationToken ct, TeamParticipantCreateArgs args)
        {
            var db = _dbFactory.Create();
            var teamParticipant = TeamParticipantDocument.Create(Uuid.NewMongo(),
                args.UserId,
                args.Username,
                args.TeamId,
                args.AvatarUrl);
            await db.TeamParticipants.InsertOneAsync(teamParticipant, cancellationToken: ct);
            return TeamParticipantDocument.ToDomain(teamParticipant);
        }

        public async Task<TeamParticipantEntity[]> TeamParticipantListOfTeams(CancellationToken ct, string[] teamIds)
        {
            var db = _dbFactory.Create();

            var participants = await db.TeamParticipants.Find(
                _f.In(x => x.TeamId, teamIds)
            ).ToListAsync(ct);

            return participants.Select(x => TeamParticipantDocument.ToDomain(x)).ToArray();
        }
    }
}