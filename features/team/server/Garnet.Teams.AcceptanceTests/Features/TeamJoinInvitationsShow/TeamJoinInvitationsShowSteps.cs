namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvitationsShow
{
    [Binding]
    public class TeamJoinInvitationsShowSteps : BaseSteps
    {
        public TeamJoinInvitationsShowSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"'(.*)' просматривает список приглашений команды '(.*)'")]
        public Task ThenПросматриваетСписокПриглашенийКоманды(string username, string teamName)
        {
            return Task.CompletedTask;
        }

        [Then(@"количество приглашений в списке равно '(.*)'")]
        public Task ThenКоличествоПриглашенийВСпискеРавно(int joinTeamCount)
        {
            return Task.CompletedTask;
        }
    }
}