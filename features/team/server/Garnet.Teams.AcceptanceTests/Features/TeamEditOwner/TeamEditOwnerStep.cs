namespace Garnet.Teams.AcceptanceTests.Features.TeamEditOwner
{
    [Binding]
    public class TeamEditOwnerStep : BaseSteps
    {
        public TeamEditOwnerStep(StepsArgs args) : base(args)
        {
        }

        [When(@"'([^']*)' изменяет владельца команды '([^']*)' на пользователя '([^']*)'")]
        public Task WhenИзменяетВладельцаКомандыНаПользователя(string username, string teamName, string newOwnerUsername)
        {
            return Task.CompletedTask;
        }
    }
}