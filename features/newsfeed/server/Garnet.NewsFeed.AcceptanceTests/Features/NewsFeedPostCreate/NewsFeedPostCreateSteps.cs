using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.AcceptanceTests.Support;
using Garnet.NewsFeed.Application.NewsFeedPost;
using Garnet.NewsFeed.Infrastructure.Api.NewsFeedPostCreate;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.NewsFeed.AcceptanceTests.Features.NewsFeedPostCreate
{
    [Binding]
    public class NewsFeedPostCreateSteps : BaseSteps
    {
        private readonly QueryExceptionsContext _queryExceptionsContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly INewsFeedPostRepository _newsFeedPostRepository;

        public NewsFeedPostCreateSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            INewsFeedPostRepository newsFeedPostRepository,
            StepsArgs args) : base(args)
        {
            _newsFeedPostRepository = newsFeedPostRepository;
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [Given(@"существует команда '(.*)'")]
        public async Task GivenСуществуетКоманда(string teamName)
        {
            var team = new NewsFeedTeamBuilder().WithTeamId(teamName);
            await Db.NewsFeedTeam.InsertOneAsync(
                team
            );
        }

        [Given(@"существует пользователь '(.*)'")]
        public Task GivenСуществуетПользователь(string username)
        {
            _currentUserProviderFake.RegisterUser(username, Uuid.NewGuid());
            return Task.CompletedTask;
        }

        [Given(@"пользователь '(.*)' является участником команды '(.*)'")]
        public async Task GivenПользовательЯвляетсяУчастникомКоманды(string username, string teamName)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var participant = new NewsFeedTeamParticipantBuilder().WithUserId(userId).WithTeamId(teamName);
            await Db.NewsFeedTeamParticipant.InsertOneAsync(participant);
        }

        [When(@"пользователь '(.*)' создает пост с содержанием '(.*)' в ленте команды '(.*)'")]
        public async Task WhenПользовательСоздаетПостССодержаниемВЛентеКоманды(string username, string content, string teamName)
        {
            var input = new NewsFeedPostCreateInput(teamName, content);

            _currentUserProviderFake.LoginAs(username);
            try
            {
                await Mutation.NewsFeedPostCreate(input);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"количество постов в ленте команды '(.*)' равно '(.*)'")]
        public async Task ThenКоличествоПостовВЛентеКомандыРавно(string teamName, int postCount)
        {
            var posts = await Db.NewsFeedPost.Find(x => x.TeamId == teamName).ToListAsync();
            posts.Count().Should().Be(postCount);
        }

        [Then(@"пользователь получает ошибку '(.*)'")]
        public Task ThenПользовательПолучаетОшибку(string errorCode)
        {
            var validError = _queryExceptionsContext.QueryExceptions.First().Errors.Any(x => x.Code == errorCode);
            validError.Should().BeTrue();
            return Task.CompletedTask;
        }
    }
}