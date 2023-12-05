using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Api.Cancellation;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.Application.NewsFeedPost;
using Garnet.NewsFeed.Application.NewsFeedPost.Args;
using MongoDB.Driver;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedPost
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

        public async Task<NewsFeedPostEntity> CreatePost(NewsFeedPostCreateArgs args)
        {
            var db = _dbFactory.Create();

            var post = NewsFeedPostDocument.Create(Uuid.NewMongo(), args.Content, args.TeamId);
            post = await InsertOneDocument(
                _ct,
                db.NewsFeedPost,
                post
            );

            return NewsFeedPostDocument.ToDomain(post);
        }

        public async Task DeletePostsByTeam(string teamId)
        {
            var db = _dbFactory.Create();
            await db.NewsFeedPost.DeleteManyAsync(
                _f.Eq(x => x.TeamId, teamId),
                _ct
            );
        }

        public async Task<NewsFeedPostEntity[]> GetPostList(string teamId, int skip, int take)
        {
            var db = _dbFactory.Create();

            var posts = await db.NewsFeedPost
            .Find(_f.Eq(x => x.TeamId, teamId))
            .Skip(skip)
            .Limit(take)
            .ToListAsync(_ct);

            return posts.Select(x => NewsFeedPostDocument.ToDomain(x)).ToArray();
        }
    }
}