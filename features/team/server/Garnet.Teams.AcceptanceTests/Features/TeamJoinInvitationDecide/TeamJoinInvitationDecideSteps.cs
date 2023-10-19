using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvitationDecide;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvitationDecide
{
    [Binding]
    public class TeamJoinInvitationDecideSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public TeamJoinInvitationDecideSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"'(.*)' принимает приглашение в команду '(.*)'")]
        public async Task WhenПринимаетПриглашениеВКоманду(string username, string teamName)
        {
            _currentUserProviderFake.LoginAs(username);

            var invitation = await Db.TeamJoinInvitations.Find(x =>
                x.UserId == _currentUserProviderFake.GetUserIdByUsername(username)
            ).FirstAsync();

            var input = new TeamJoinInvitationDecideInput(invitation.Id, true);
            await Mutation.TeamJoinInvitationDecide(CancellationToken.None, input);
        }

        [When(@"'(.*)' отклоняет приглашение в команду '(.*)'")]
        public async Task WhenОтклоняетПриглашениеВКоманду(string username, string teamName)
        {
            _currentUserProviderFake.LoginAs(username);

            var invitation = await Db.TeamJoinInvitations.Find(x =>
                x.UserId == _currentUserProviderFake.GetUserIdByUsername(username)
            ).FirstAsync();

            var input = new TeamJoinInvitationDecideInput(invitation.Id, false);
            await Mutation.TeamJoinInvitationDecide(CancellationToken.None, input);
        }
    }
}