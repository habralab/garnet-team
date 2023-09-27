using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamDocument> _f = Builders<TeamDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamDocument> _u = Builders<TeamDocument>.Update;
        private readonly IndexKeysDefinitionBuilder<TeamDocument> _i = Builders<TeamDocument>.IndexKeys;

        public TeamRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<Team> CreateTeam(CancellationToken ct, string name, string description, string ownerUserId)
        {
            var db = _dbFactory.Create();
            var team = TeamDocument.Create(
             Uuid.NewMongo(),
             name,
             description,
             ownerUserId
            );
            await db.Teams.InsertOneAsync(team, cancellationToken: ct);
            return TeamDocument.ToDomain(team);
        }

        public async Task<Team?> GetTeamById(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();
            var team = await db.Teams.Find(o => o.Id == teamId).FirstOrDefaultAsync(ct);
            return team is not null ? TeamDocument.ToDomain(team) : null;
        }

        public async Task CreateIndexes(CancellationToken ct)
        {
            var db = _dbFactory.Create();
            await db.Teams.Indexes.CreateOneAsync(
                new CreateIndexModel<TeamDocument>(
                    _i.Text(o => o.Name)
                        .Text(o => o.Description)
                        .Text(o => o.OwnerUserId)
                ),
                cancellationToken: ct);
        }
    }
}