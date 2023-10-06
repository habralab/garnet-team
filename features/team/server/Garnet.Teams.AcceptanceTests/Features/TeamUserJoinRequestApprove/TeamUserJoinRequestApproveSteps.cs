namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestApprove
{
    [Binding]
    public class TeamUserJoinRequestApproveSteps : BaseSteps
    {
        public TeamUserJoinRequestApproveSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"'(.*)' принимает заявку на вступление в команду '(.*)' от пользователя '(.*)'")]
        public Task WhenПринимаетЗаявкуНаВступлениеВКомандуОтПользоваьтеля(string ownerUsername, string teamName, string username)
        {
            return Task.CompletedTask;
        }

        [When(@"'(.*)' отклоняет заявку на вступление в команду '(.*)' от пользователя '(.*)'")]
        public Task WhenОтклоняетЗаявкуНаВступлениеВКомандуОтПользоваьтеля(string ownerUsername, string teamName, string username)
        {
            return Task.CompletedTask;
        }

        [Then(@"в команде '(.*)' количество участников равно '(.*)'")]
        public Task ThenВКомандеКоличествоУчастниковРавно(string teamName, int joinRequestCount)
        {
            return Task.CompletedTask;
        }
    }
}