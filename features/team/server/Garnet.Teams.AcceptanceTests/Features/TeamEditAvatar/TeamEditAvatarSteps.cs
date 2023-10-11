namespace Garnet.Teams.AcceptanceTests.Features.TeamEditAvatar
{
    [Binding]
    public class TeamEditAvatarSteps : BaseSteps
    {
        public TeamEditAvatarSteps(StepsArgs args) : base(args)
        {
        }

        [Given(@"аватаркой команды '(.*)' является ссылка '(.*)'")]
        public Task GivenАватаркойПроектаЯвляется(string teamName, string avatar)
        {
            return Task.CompletedTask;
        }

        [When(@"'(.*)' редактирует аватарку проекта '(.*)' на '(.*)'")]
        public Task WhenРедактируетАватаркуПроектаНа(string username, string teamName, string avatar)
        {
            return Task.CompletedTask;
        }

        [Then(@"аватаркой команды '(.*)' является ссылка '(.*)'")]
        public Task ThenАватаркойПроектаЯвляетсяСсылка(string teamName, string avatar)
        {
            return Task.CompletedTask;
        }

        [Then(@"в удаленном хранилище для команды '(.*)' есть файл '(.*)'")]
        public Task ThenВУдаленномХранилищеДляКоманлыЕстьФайл(string teamName, string avatar)
        {
            return Task.CompletedTask;
        }

        [Then(@"в системе аватарка команды '(.*)' содержит ссылку '(.*)'")]
        public Task ThenВСистемеАватаркаКомандыСодержитСсылку(string teamName, string avatar)
        {
            return Task.CompletedTask;
        }
    }
}