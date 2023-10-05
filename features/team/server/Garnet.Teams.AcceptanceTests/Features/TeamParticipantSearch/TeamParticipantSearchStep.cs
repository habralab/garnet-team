using FluentAssertions;
using Garnet.Teams.Infrastructure.Api.TeamParticipantSearch;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamParticipantSearch
{
    [Binding]
    public class TeamParticipantSearchStep : BaseSteps
    {
        private TeamParticipantFilterPayload _result = null!;
        public TeamParticipantSearchStep(StepsArgs args) : base(args)
        {
        }

        [When(@"осуществляется поиск по участникам команды '([^']*)' с запросом '([^']*)'")]
        public async Task WhenОсуществляетсяПоисПоУчастникамКомандыСЗапросом(string teamName, string query)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var input = new TeamParticipantFilterInput(team.Id, query, 0, 100);

            _result = await Query.TeamParticipantFilter(CancellationToken.None, input);
        }

        [Then(@"в результатах поиска отображается '([^']*)' участник команды")]
        public Task ThenВРезультатхПоискаОтображаетсяУчастникКоманды(int resultCount)
        {
            _result.TeamParticipants.Count().Should().Be(resultCount);
            return Task.CompletedTask;
        }
    }
}