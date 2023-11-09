using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Api.Cancellation;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.NewsFeed.Application;
using MongoDB.Driver;

namespace Garnet.NewsFeed.Infrastructure.MongoDB
{
    public class NewsFeedPostRepository : RepositoryBase, INewsFeedPostRepository
    {
        private readonly CancellationToken _ct;
        private readonly DbFactory _dbFactory;
        private readonly FilterDefinitionBuilder<NewsFeedPostDocument> _f = Builders<NewsFeedPostDocument>.Filter;

        public NewsFeedPostRepository(
            DbFactory dbFactory,
            ICurrentUserProvider currentUserProvider,
            IDateTimeService dateTimeService,
            CancellationTokenProvider ctp) : base(currentUserProvider, dateTimeService)
        {
            _dbFactory = dbFactory;
            _ct = ctp.Token;
        }

        public Task<NewsFeedPostEntity> CreatePost(string content, string authorUserId)
        {
            throw new NotImplementedException();
        }

        public Task<NewsFeedPostEntity[]> GetPostList(string teamId, int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}