using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;

namespace Garnet.Teams.AcceptanceTests.Features.TeamGet
{
    [Binding]
    [Scope(Scenario = "Просмотр карточки команды")]

    public class TeamGetSteps : BaseStepsWithGivenUser
    {
        public TeamGetSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(currentUserProviderFake, args) { }

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