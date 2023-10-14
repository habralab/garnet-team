using FluentAssertions;
using Garnet.Teams.Infrastructure.Api.TeamsList;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamsList
{
    [Binding]
    public class TeamsListSteps : BaseSteps
    {
        private TeamsListPayload _result;
        public TeamsListSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"производится запрос списка команд пользователя '(.*)'")]
        public async Task WhenПроизводитсяЗапросСпискаКомандПользователя(string username)
        {
            var user = await Db.TeamUsers.Find(x=> x.Username == username).FirstAsync();

            var input = new TeamsListInput(user.Id, 0, 10);
            _result = await Query.TeamsList(CancellationToken.None, input);
        }

        [Then(@"количество результатов списка команд пользователя равно '(.*)'")]
        public Task ThenКоличествоРезультатовСпискаКомандПользователяРавно(int teamsCount)
        {
            _result.Teams.Length.Should().Be(teamsCount);
            return Task.CompletedTask;
        }
    }
}