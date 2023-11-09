using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.Application.NewsFeedPost.Args;
using Garnet.NewsFeed.Application.NewsFeedPost.Commands;
using Garnet.NewsFeed.Infrastructure.Api.NewsFeedPostCreate;
using HotChocolate.Types;

namespace Garnet.NewsFeed.Infrastructure.Api
{
    [ExtendObjectType("Mutation")]
    public class NewsFeedMutation
    {
        private readonly NewsFeedPostCreateCommand _newsFeedPostCreateCommand;

        public NewsFeedMutation(NewsFeedPostCreateCommand newsFeedPostCreateCommand)
        {
            _newsFeedPostCreateCommand = newsFeedPostCreateCommand;
        }

        public async Task<NewsFeedPostPayload> NewsFeedPostCreate(NewsFeedPostCreateInput input)
        {
            var args = new NewsFeedPostCreateArgs(input.TeamId, input.Content);
            var result = await _newsFeedPostCreateCommand.Execute(args);
            result.ThrowQueryExceptionIfHasErrors();

            var post = result.Value;
            return new NewsFeedPostPayload(
                post.Id,
                post.TeamId,
                post.AuditInfo.CreatedBy,
                post.AuditInfo.CreatedAt,
                post.Content
            );
        }
    }
}