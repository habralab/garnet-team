using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.Api.TeamGet;

namespace Garnet.Teams.AcceptanceTests.Features.TeamGet
{
    [Binding]
    public class TeamGetSteps : BaseStepsWithGivenUser
    {
        private TeamGetPayload _teamGetPayload;
        private Exception? _exception;
        private string _id;

        public TeamGetSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(currentUserProviderFake, args) { }

        [Given(@"существует команда '([^']*)' с владельцем '([^']*)' и описанием '([^']*)'")]
        public async Task GivenСуществуетКомандаСВладельцемИОписанием(string team, string ownerUsername, string description)
        {
            _team = GiveMe.Team().WithName(team).WithDescription(description);
            await Db.Teams.InsertOneAsync(_team);
        }

        [Scope(Tag = "Команда_найдена")]
        [When(@"'([^']*)' открывает карточку команды '([^']*)'")]
        public async Task WhenОткрываетКарточкуКоманды(string username, string team)
        {
            _teamGetPayload = await Query.TeamGet(CancellationToken.None, _team.Id);
        }

        [Scope(Tag = "Команда_найдена")]
        [Then(@"описание команды в карточке состоит из '([^']*)'")]
        public Task ThenОписаниеКомандыВКарточкеСостоитИз(string description)
        {
            _teamGetPayload.Description.Should().Be(_team.Description);
            return Task.CompletedTask;
        }

        [Scope(Tag = "Команда_не_найдена")]
        [When(@"пользователь открывает карточку команды, которой нет в системе")]
        public async Task WhenПользовательОткрываетКарточкуКомандыКоторойНетВСистеме()
        {
            _id = Uuid.NewMongo();
            try
            {
                await Query.TeamGet(CancellationToken.None, _id);
            }
            catch (System.Exception ex)
            {
                _exception = ex;
            }
        }

        [Scope(Tag = "Команда_не_найдена")]
        [Then(@"происходит ошибка '(.*)'")]
        public Task ThenПроисходитОшибка(string error)
        {
            var errorMsg = error.Replace("ID", _id);
            _exception!.Message.Should().Be(errorMsg);
            return Task.CompletedTask;
        }
    }
}