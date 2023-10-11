using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.Api.TeamCreate;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.TeamUser;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamCreate
{
    [Binding]
    public class TeamCreateSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        public TeamCreateSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"существует пользователь '([^']*)'")]
        public async Task GivenСуществуетПользователь(string username)
        {
            var user = TeamUserDocument.Create(Uuid.NewMongo(), username);
            await Db.TeamUsers.InsertOneAsync(user);
            _currentUserProviderFake.RegisterUser(username, user.Id);
        }

        [When(@"пользователь '([^']*)' создает команду '([^']*)'")]
        public async Task WhenПользовательСоздаетКоманду(string username, string team)
        {
            var input = new TeamCreateInput(team, string.Empty, Array.Empty<string>());
            await Mutation.TeamCreate(CancellationToken.None, _currentUserProviderFake.LoginAs(username), input);
        }

        [Then(@"в системе присутствует команда '([^']*)'")]
        public async Task ThenВСистемеПрисутствуетКоманда(string team)
        {
            var newTeam = await Db.Teams.Find(x => x.Name == team).FirstOrDefaultAsync();
            newTeam.Should().NotBeNull();
        }

        [Then(@"пользователь '([^']*)' является владельцем команды '([^']*)'")]
        public async Task ThenПользовательЯвляетсяВладельцемКоманды(string username, string team)
        {
            var newTeam = await Db.Teams.Find(x => x.Name == team).FirstOrDefaultAsync();
            newTeam.OwnerUserId.Should().Be(_currentUserProviderFake.GetUserIdByUsername(username));
        }

        [Then(@"пользователь '([^']*)' является участником команды '([^']*)'")]
        public async Task ThenПользовательЯвляетсяУчастникомКоманды(string username, string team)
        {
            var newTeam = await Db.Teams.Find(x => x.Name == team).FirstOrDefaultAsync();
            var participants = await Db.TeamParticipants.Find(x => x.TeamId == newTeam.Id).ToListAsync();

            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var userIsParticipant = participants.Any(x => x.UserId == userId);

            userIsParticipant.Should().BeTrue();
        }
    }
}