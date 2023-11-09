using Garnet.NewsFeed.Infrastructure.Api.NewsFeedPostCreate;
using HotChocolate.Types;

namespace Garnet.NewsFeed.Infrastructure.Api
{
    [ExtendObjectType("Mutation")]
    public class NewsFeedMutation
    {
        public Task<NewsFeedPostPayload> NewsFeedPostCreate(NewsFeedPostCreateInput input)
        {
            return null;
        }
    }
}