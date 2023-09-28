namespace Garnet.Teams.AcceptanceTests.Features.TeamGet
{
    [Binding]
    public class TeamGetSteps : BaseSteps
    {
        public TeamGetSteps(StepsArgs args) : base(args)
        {
        }

        [Given(@"существует пользователь '([^']*)'")]
        public async Task GivenСуществуетПользователь(string username)
        {

        }

        [Given(@"существует команда '([^']*)' с владельцем '([^']*)' и описанием '([^']*)'")]
        public async Task GivenСуществуетКомандаСВладельцемИОписанием(string team, string ownerUsername, string description)
        {

        }

        [When(@"'([^']*)' открывает карточку команды '([^']*)'")]
        public async Task WhenОткрываетКарточкуКоманды(string username, string team)
        {

        }

        [Then(@"описание команды в карточке состоит из '([^']*)'")]
        public async Task ThenОписаниеКомандыВКарточкеСостоитИз(string description)
        {

        }
    }
}