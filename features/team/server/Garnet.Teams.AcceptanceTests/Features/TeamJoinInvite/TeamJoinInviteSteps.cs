namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvite
{
    [Binding]
    public class TeamJoinInviteSteps : BaseSteps
    {
        public TeamJoinInviteSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"пользователь '(.*)' приглашает '(.*)' в команду '(.*)'")]
        public Task WhenПользовательПриглашаетВКоманду(string teamOwner, string username, string teamName)
        {
            return Task.CompletedTask;
        }

        [Then(@"у пользователя '(.*)' количество приглашений в команды равно '(.*)'")]
        public Task ThenУПользователяКоличествоПриглашенийВКомандыРавно(string username, int joinInviteCount)
        {
            return Task.CompletedTask;
        }

        [Given(@"существует приглашение пользователя '(.*)' на вступление в команду '(.*)' от владельца")]
        public Task GivenСущесвуетПриглашениеПользователяНаВступлениеВКомандуОтВладельца(string username, string teamName)
        {
            return Task.CompletedTask;
        }
    }
}