namespace Garnet.Teams.AcceptanceTests.Features.TeamParticipantSearch
{
    [Binding]
    public class TeamParticipantSearchStep : BaseSteps
    {
        public TeamParticipantSearchStep(StepsArgs args) : base(args)
        {
        }

        [When(@"осуществляется поиск по участникам команды '([^']*)' с запросом '([^']*)'")]
        public Task WhenОсуществляетсяПоисПоУчастникамКомандыСЗапросом(string teamName, string query)
        {
            return Task.CompletedTask;
        }

        [Then(@"в результатах поиска отображается '([^']*)' участник команды")]
        public Task ThenВРезультатхПоискаОтображаетсяУчастникКоманды(int resultCount)
        {
            return Task.CompletedTask;
        }
    }
}