using FluentAssertions;
using Garnet.Teams.Infrastructure.Api.TeamsList;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamsListByUserQuery
{
    [Binding]
    public class TeamsListByUserSteps : BaseSteps
    {
        private TeamsListPayload _result = null!;
        public TeamsListByUserSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"производится запрос списка команд пользователя '(.*)'")]
        public async Task WhenПроизводитсяЗапросСпискаКомандПользователя(string username)
        {
            var user = await Db.TeamUsers.Find(x=> x.Username == username).FirstAsync();

            var input = new TeamsListInput(user.Id, 0, 10);
            _result = await Query.TeamsListByUser(CancellationToken.None, input);
        }

        [Then(@"количество команд пользователя в результате равно '(.*)'")]
        public Task ThenКоличествоКомандКомандПользователяРавно(int teamsCount)
        {
            _result.Teams.Length.Should().Be(teamsCount);
            return Task.CompletedTask;
        }
    }
}