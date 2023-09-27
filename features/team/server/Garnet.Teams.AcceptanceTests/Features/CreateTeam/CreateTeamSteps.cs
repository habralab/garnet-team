using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Features.Support;

namespace Garnet.Teams.AcceptanceTests.Features.CreateTeam
{
    [Binding]
    public class CreateTeamSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private UserDocumentBuilder _user;

        public CreateTeamSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"существует пользователь '([^']*)'")]
        public async Task GivenСуществуетПользователь(string username)
        {
            var id = _currentUserProviderFake.RegisterUser(username, Uuid.NewMongo());
            _user = GiveMe.User().WithId(id).WithUserName(username);
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

        [Then(@"пользователь '([^']*)' является участником команды '([^']*)'")]
        public async Task ThenПользовательЯвляетсяУчастникомКоманды(string username, string team)
        {

        }
    }
}