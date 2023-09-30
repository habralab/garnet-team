using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.Api.TeamGet;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamGet
{
    [Binding]
    public class TeamGetSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private TeamPayload _teamGetPayload = null!;
        private Exception? _exception;
        private string _id = null!;

        public TeamGetSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"существует команда '([^']*)'")]
        public async Task GivenСуществует(string teamName)
        {
            var team = GiveMe.Team().WithName(teamName);
            await Db.Teams.InsertOneAsync(team);
        }

        [When(@"'([^']*)' открывает карточку команды '([^']*)'")]
        public async Task WhenОткрываетКарточкуКоманды(string username, string teamName)
        {
            var team = await Db.Teams.Find(x=> x.Name == teamName).FirstAsync();
            _currentUserProviderFake.LoginAs(username);
            _teamGetPayload = await Query.TeamGet(CancellationToken.None, team.Id);
        }

        [Then(@"описание команды в карточке состоит из '([^']*)'")]
        public Task ThenОписаниеКомандыВКарточкеСостоитИз(string description)
        {
            _teamGetPayload.Description.Should().Be(description);
            return Task.CompletedTask;
        }

        [Then(@"имя команды в карточке состоит из '([^']*)'")]
        public Task ThenмяКомандыВКарточкеСостоитИз(string name)
        {
            _teamGetPayload.Name.Should().Be(name);
            return Task.CompletedTask;
        }

        [When(@"'([^']*)' открывает карточку команды, которой нет в системе")]
        public async Task WhenПользовательОткрываетКарточкуКомандыКоторойНетВСистеме(string username)
        {
            _currentUserProviderFake.LoginAs(username);
            _id = Uuid.NewMongo();
            try
            {
                await Query.TeamGet(CancellationToken.None, _id);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"происходит ошибка '(.*)'")]
        public Task ThenПроисходитОшибка(string error)
        {
            var errorMsg = error.Replace("ID", _id);
            _exception!.Message.Should().Be(errorMsg);
            return Task.CompletedTask;
        }
    }
}