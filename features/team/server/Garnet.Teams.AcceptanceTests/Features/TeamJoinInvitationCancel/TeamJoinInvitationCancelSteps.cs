using FluentAssertions;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvitationCancel
{
    [Binding]
    public class TeamJoinInvitationCancelSteps : BaseSteps
    {
        public TeamJoinInvitationCancelSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"'(.*)' отменяет приглашение пользователя '(.*)' на вступление в команду '(.*)'")]
        public Task WhenОтменяетПриглашениеПользователяНаВступлениеВКоманду(string ownerUsername, string username, string teamName)
        {
            return Task.CompletedTask;
        }

        [Then(@"в команде '(.*)' количество приглашений на вступление равно '(.*)'")]
        public async Task ThenВКомандеКоличествоПриглашенийНаВступлениеРавно(string teamName, int invitationCount)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var invitations = await Db.TeamJoinInvitations.Find(x => x.TeamId == team.Id).ToListAsync();

            invitations.Count.Should().Be(invitationCount);
        }
    }
}