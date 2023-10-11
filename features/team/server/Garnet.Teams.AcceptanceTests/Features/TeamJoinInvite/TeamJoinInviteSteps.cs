using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.TeamJoinInvitation;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamJoinInvite
{
    [Binding]
    public class TeamJoinInviteSteps : BaseSteps
    {
        private readonly QueryExceptionsContext _errorStepContext;
        private readonly CurrentUserProviderFake _currentUserProviderFake;


        public TeamJoinInviteSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext errorStepContext,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _errorStepContext = errorStepContext;
        }

        [When(@"пользователь '(.*)' приглашает '(.*)' в команду '(.*)'")]
        public async Task WhenПользовательПриглашаетВКоманду(string teamOwner, string username, string teamName)
        {
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            var claims = _currentUserProviderFake.LoginAs(teamOwner);
            var input = new TeamJoinInviteInput(userId, team.Id);

            try
            {
                await Mutation.TeamJoinInvite(CancellationToken.None, claims, input);
            }
            catch (QueryException ex)
            {
                _errorStepContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"у пользователя '(.*)' количество приглашений в команды равно '(.*)'")]
        public async Task ThenУПользователяКоличествоПриглашенийВКомандыРавно(string username, int joinInviteCount)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var invitations = await Db.TeamJoinInvitations.Find(x => x.UserId == user.Id).ToListAsync();
            invitations.Count.Should().Be(joinInviteCount);
        }

        [Given(@"существует приглашение пользователя '(.*)' на вступление в команду '(.*)' от владельца")]
        public async Task GivenСущесвуетПриглашениеПользователяНаВступлениеВКомандуОтВладельца(string username, string teamName)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            var invitation = TeamJoinInvitationDocument.Create(Uuid.NewMongo(), user.Id, team.Id);
            await Db.TeamJoinInvitations.InsertOneAsync(invitation);
        }
    }
}