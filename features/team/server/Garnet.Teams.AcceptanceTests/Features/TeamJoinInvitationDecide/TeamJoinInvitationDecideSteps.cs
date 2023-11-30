using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.FakeServices.NotificationFake;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvitationDecide;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvitationDecide
{
    [Binding]
    public class TeamJoinInvitationDecideSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly SendNotificationCommandMessageFakeConsumer _sendNotificationCommandMessageFakeConsumer;

        public TeamJoinInvitationDecideSteps(
            CurrentUserProviderFake currentUserProviderFake,
            SendNotificationCommandMessageFakeConsumer sendNotificationCommandMessageFakeConsumer,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _sendNotificationCommandMessageFakeConsumer = sendNotificationCommandMessageFakeConsumer;
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

        [Then(@"в последнем уведомлении для пользователя '(.*)' связанной сущностью является пользователь '(.*)'")]
        public async Task ThenВПоследнемУведомленииДляПользователяСвязаннойСущностьюЯвляетсяПользователь(string ownerUsername, string username)
        {
            var owner = await Db.TeamUsers.Find(x => x.Username == ownerUsername).FirstAsync();
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var message = _sendNotificationCommandMessageFakeConsumer.Notifications
            .Last(x => x.UserId == owner.Id);
            message.QuotedEntities.Should().Contain(x=> x.Id == user.Id);
        }
    }
}