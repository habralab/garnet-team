namespace Garnet.Teams.AcceptanceTests.Features.TeamParticipantLeaveTeam
{
    [Binding]
    public class TeamParticipantLeaveTeamSteps : BaseSteps
    {
        public TeamParticipantLeaveTeamSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"'(.*)' выходит из состава команды '(.*)'")]
        public Task WhenВыходитИзСоставаКоманды(string username, string teamName)
        {
            return Task.CompletedTask;
        }
    }
}