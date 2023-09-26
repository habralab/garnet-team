namespace Garnet.Teams.AcceptanceTests.Features.CreateTeam
{
    [Binding]
    public class CreateTeamSteps : BaseSteps
    {
        public CreateTeamSteps(StepsArgs args) : base(args)
        {

        }

        [Given(@"существует пользователь '([^']*)'")]
        public async Task GivenСуществуетПользователь(string username)
        {

        }

        [When(@"пользователь '([^']*)' создает команду '([^']*)'")]
        public async Task WhenПользовательСоздаетКоманду(string username, string team)
        {

        }

        [Then(@"в системе присутствует команда '([^']*)'")]
        public async Task ThenВСистемеПрисутствуетКоманда(string team)
        {

        }

        [Then(@"пользователь '([^']*)' является владельцем команды '([^']*)'")]
        public async Task ThenПользовательЯвляетсяВладельцемКоманды(string username, string team)
        {

        }
    }
}