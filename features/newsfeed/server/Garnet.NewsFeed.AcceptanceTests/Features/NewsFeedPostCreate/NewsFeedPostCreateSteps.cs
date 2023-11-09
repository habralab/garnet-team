using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using TechTalk.SpecFlow;

namespace Garnet.NewsFeed.AcceptanceTests.Features.NewsFeedPostCreate
{
    [Binding]
    public class NewsFeedPostCreateSteps : BaseSteps
    {
        private readonly QueryExceptionsContext _queryExceptionsContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public NewsFeedPostCreateSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [Given(@"существует команда '(.*)'")]
        public Task GivenСуществуетКоманда(string teamName)
        {
            return Task.CompletedTask;
        }

        [Given(@"существует пользователь '(.*)'")]
        public Task GivenСуществуетПользователь(string username)
        {
            return Task.CompletedTask;
        }

        [Given(@"пользователь '(.*)' является участником команды '(.*)'")]
        public Task GivenПользовательЯвляетсяУчастникомКоманды(string username, string teamName)
        {
            return Task.CompletedTask;
        }

        [When(@"пользователь '(.*)' создает пост с содержанием '(.*)' в ленте команды '(.*)'")]
        public Task WhenПользовательСоздаетПостССодержаниемВЛентеКоманды(string username, string content, string teamName)
        {
            return Task.CompletedTask;
        }

        [Then(@"количество постов в ленте команды '(.*)' равно '(.*)'")]
        public Task ThenКоличествоПостовВЛентеКомандыРавно(string teamName, string postCount)
        {
            return Task.CompletedTask;
        }

        [Then(@"пользователь получает ошибку '(.*)'")]
        public Task ThenПользовательПолучаетОшибку(string errorMsg)
        {
            return Task.CompletedTask;
        }
    }
}