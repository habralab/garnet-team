using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.AcceptanceTests.Features.TeamGet
{
    [Binding]
    [Scope(Scenario = "Просмотр карточки команды")]

    public class TeamGetSteps : BaseStepsWithGivenUser
    {
        private TeamGetPayload _teamGetPayload;
        
        public TeamGetSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(currentUserProviderFake, args) { }

        [Given(@"существует команда '([^']*)' с владельцем '([^']*)' и описанием '([^']*)'")]
        public async Task GivenСуществуетКомандаСВладельцемИОписанием(string team, string ownerUsername, string description)
        {
            _team = GiveMe.Team().WithName(team).WithDescription(description);
            await Db.Teams.InsertOneAsync(_team);
        }

        [When(@"'([^']*)' открывает карточку команды '([^']*)'")]
        public async Task WhenОткрываетКарточкуКоманды(string username, string team)
        {
            _teamGetPayload = await Query.TeamGet(CancellationToken.None, _currentUserProviderFake.LoginAs(username), _team.Id);
        }

        [Then(@"описание команды в карточке состоит из '([^']*)'")]
        public Task ThenОписаниеКомандыВКарточкеСостоитИз(string description)
        {
            _teamGetPayload.Description.Should().Be(_team.Description);
            return Task.CompletedTask;
        }
    }
}