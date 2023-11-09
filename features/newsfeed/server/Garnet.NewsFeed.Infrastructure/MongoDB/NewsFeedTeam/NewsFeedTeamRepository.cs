using Garnet.Common.Infrastructure.Api.Cancellation;
using Garnet.NewsFeed.Application.NewsFeedTeam;
using MongoDB.Driver;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeam
{
    public class NewsFeedTeamRepository : INewsFeedTeamRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly CancellationToken _ct;
        private readonly FilterDefinitionBuilder<NewsFeedTeamDocument> _f = Builders<NewsFeedTeamDocument>.Filter;
        private readonly UpdateDefinitionBuilder<NewsFeedTeamDocument> _u = Builders<NewsFeedTeamDocument>.Update;

        public NewsFeedTeamRepository(DbFactory dbFactory, CancellationTokenProvider ctp)
        {
            _dbFactory = dbFactory;
            _ct = ctp.Token;
        }

        public async Task CreateTeam(string id, string ownerUserId)
        {
            var db = _dbFactory.Create();
            var team = NewsFeedTeamDocument.Create(id, ownerUserId);
            await db.NewsFeedTeam.InsertOneAsync(team, cancellationToken: _ct);
        }

        public async Task DeleteTeam(string id)
        {
            var db = _dbFactory.Create();
            await db.NewsFeedTeam.DeleteOneAsync(
                _f.Eq(x => x.Id, id),
                cancellationToken: _ct
            );
        }

        public async Task<NewsFeedTeamEntity?> GetTeamById(string id)
        {
            var db = _dbFactory.Create();
            var team = await db.NewsFeedTeam.Find(
                _f.Eq(x => x.Id, id)
            ).FirstOrDefaultAsync(_ct);

            return team is null ? null : NewsFeedTeamDocument.ToDomain(team);
        }

        public async Task UpdateTeamOwner(string id, string userId)
        {
            var db = _dbFactory.Create();
            await db.NewsFeedTeam.UpdateOneAsync(
                _f.Eq(x => x.Id, id),
                _u.Set(x => x.OwnerUserId, userId),
                cancellationToken: _ct
            );
        }
    }
}