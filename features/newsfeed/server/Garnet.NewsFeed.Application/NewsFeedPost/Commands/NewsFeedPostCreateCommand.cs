using FluentResults;
using Garnet.Common.Application;
using Garnet.NewsFeed.Application.NewsFeedPost.Args;
using Garnet.NewsFeed.Application.NewsFeedPost.Errors;
using Garnet.NewsFeed.Application.NewsFeedTeam;
using Garnet.NewsFeed.Application.NewsFeedTeam.Errors;
using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;

namespace Garnet.NewsFeed.Application.NewsFeedPost.Commands
{
    public class NewsFeedPostCreateCommand
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly INewsFeedPostRepository _newsFeedPostRepository;
        private readonly INewsFeedTeamRepository _newsFeedTeamRepository;
        private readonly INewsFeedTeamParticipantRepository _newsFeedTeamParticipantRepository;
        public NewsFeedPostCreateCommand(
            ICurrentUserProvider currentUserProvider,
            INewsFeedTeamRepository newsFeedTeamRepository,
            INewsFeedTeamParticipantRepository newsFeedTeamParticipantRepository,
            INewsFeedPostRepository newsFeedPostRepository)
        {
            _currentUserProvider = currentUserProvider;
            _newsFeedTeamRepository = newsFeedTeamRepository;
            _newsFeedPostRepository = newsFeedPostRepository;
            _newsFeedTeamParticipantRepository = newsFeedTeamParticipantRepository;
        }

        public async Task<Result<NewsFeedPostEntity>> Execute(NewsFeedPostCreateArgs args)
        {
            var team = await _newsFeedTeamRepository.GetTeamById(args.TeamId);
            if (team is null)
            {
                return Result.Fail(new NewsFeedTeamNotFoundError(args.TeamId));
            }

            var membership = await _newsFeedTeamParticipantRepository.EnsureUserIsTeamParticipant(team.Id, _currentUserProvider.UserId);
            if (membership is null)
            {
                return Result.Fail(new NewsFeedOnlyTeamParticipantCanCreate());
            }

            var post = await _newsFeedPostRepository.CreatePost(args);
            return Result.Ok(post);
        }
    }
}