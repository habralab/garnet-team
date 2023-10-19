namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvitationDecide
{
    [Binding]
    public class TeamJoinInvitationDecideSteps : BaseSteps
    {
        public TeamJoinInvitationDecideSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"'(.*)' принимает приглашение в команду '(.*)'")]
        public Task WhenПринимаетПриглашениеВКоманду(string маша0, string fooBar1)
        {
            return Task.CompletedTask;
        }

        [When(@"'(.*)' отклоняет приглашение в команду '(.*)'")]
        public Task WhenОтклоняетПриглашениеВКоманду(string маша0, string fooBar1)
        {
            return Task.CompletedTask;
        }
    }
}