using Garnet.Common.Application;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.Team.Args;
using MongoDB.Driver;

namespace Garnet.Teams.Infrastructure.MongoDb.Team
{
    public class TeamRepository : RepositoryBase, ITeamRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<TeamDocument> _f = Builders<TeamDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamDocument> _u = Builders<TeamDocument>.Update;
        private readonly IndexKeysDefinitionBuilder<TeamDocument> _i = Builders<TeamDocument>.IndexKeys;

        public TeamRepository(
            DbFactory dbFactory,
            ICurrentUserProvider currentUserProvider,
            IDateTimeService dateTimeService) : base(currentUserProvider, dateTimeService)
        {
            _dbFactory = dbFactory;
        }

        public async Task<TeamEntity> CreateTeam(CancellationToken ct, string ownerUserId, TeamCreateArgs args)
        {
            var db = _dbFactory.Create();
            var team = TeamDocument.Create(
             Uuid.NewMongo(),
             args.Name,
             args.Description,
             ownerUserId!,
             null,
             args.Tags
            );

            team = await InsertOneDocument(ct, db.Teams, team);
            return TeamDocument.ToDomain(team);
        }

        public async Task<TeamEntity?> GetTeamById(CancellationToken ct, string teamId)
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
                ),
                cancellationToken: ct);
        }

        public async Task<TeamEntity[]> FilterTeams(CancellationToken ct, TeamFilterArgs args)
        {
            var db = _dbFactory.Create();

            var searchFilter = args.Search is null
                ? _f.Empty
                : _f.Where(x => x.Description.ToLower().Contains(args.Search.ToLower()) || x.Name.ToLower().Contains(args.Search.ToLower()));

            var tagsFilter = args.Tags.Length > 0
                ? _f.All(o => o.Tags, args.Tags)
                : _f.Empty;

            var teams = await db.Teams
                .Find(searchFilter & tagsFilter)
                .Skip(args.Skip)
                .Limit(args.Take)
                .ToListAsync(ct);

            return teams.Select(x => TeamDocument.ToDomain(x)).ToArray();
        }

        public async Task<TeamEntity?> DeleteTeam(CancellationToken ct, string teamId)
        {
            var db = _dbFactory.Create();

            var team = await db.Teams.FindOneAndDeleteAsync(
                _f.Eq(x => x.Id, teamId)
            );

            return team is null ? null : TeamDocument.ToDomain(team);
        }

        public async Task<TeamEntity?> EditTeamDescription(CancellationToken ct, string teamId, string description)
        {
            var db = _dbFactory.Create();

            var team = await FindOneAndUpdateDocument(
                ct,
                db.Teams,
                _f.Eq(x => x.Id, teamId),
                _u.Set(x => x.Description, description)
            );

            return team is null ? null : TeamDocument.ToDomain(team);
        }

        public async Task<TeamEntity?> EditTeamOwner(CancellationToken ct, string teamId, string newOwnerUserId)
        {
            var db = _dbFactory.Create();

            var team = await FindOneAndUpdateDocument(
                ct,
                db.Teams,
                _f.Eq(x => x.Id, teamId),
                _u.Set(x => x.OwnerUserId, newOwnerUserId)
            );

            return team is null ? null : TeamDocument.ToDomain(team);
        }

        public async Task<TeamEntity?> EditTeamAvatar(CancellationToken ct, string teamId, string avatarUrl)
        {
            var db = _dbFactory.Create();

            var team = await FindOneAndUpdateDocument(
                ct,
                db.Teams,
                _f.Eq(x => x.Id, teamId),
                _u.Set(x => x.AvatarUrl, avatarUrl)
            );

            return team is null ? null : TeamDocument.ToDomain(team);
        }

        public async Task<TeamEntity?> EditTeamName(CancellationToken ct, string teamId, string name)
        {
            var db = _dbFactory.Create();

            var team = await FindOneAndUpdateDocument(
                ct,
                db.Teams,
                _f.Eq(x => x.Id, teamId),
                _u.Set(x => x.Name, name)
            );

            return team is null ? null : TeamDocument.ToDomain(team);
        }

        public async Task<TeamEntity?> EditTeamTags(CancellationToken ct, string teamId, string[] tags)
        {
            var db = _dbFactory.Create();

            var team = await db.Teams.FindOneAndUpdateAsync(
                _f.Eq(x => x.Id, teamId),
                _u.Set(x => x.Tags, tags),
                options: new FindOneAndUpdateOptions<TeamDocument>
                {
                    ReturnDocument = ReturnDocument.After
                },
                cancellationToken: ct
            );

            return team is null ? null : TeamDocument.ToDomain(team);
        }
    }
}