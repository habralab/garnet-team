using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Features.Support;
using Garnet.Teams.Infrastructure.Api.TeamCreate;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.CreateTeam
{
    [Binding]
    public class CreateTeamSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private UserDocumentBuilder _user = null!;

        public CreateTeamSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"существует пользователь '([^']*)'")]
        public Task GivenСуществуетПользователь(string username)
        {
            _user = GiveMe.User().WithId(Uuid.NewMongo());
            _currentUserProviderFake.RegisterUser(username, _user.Id);
            return Task.CompletedTask;
        }

        [When(@"пользователь '([^']*)' создает команду '([^']*)'")]
        public async Task WhenПользовательСоздаетКоманду(string username, string team)
        {
            var input = new TeamCreateInput(team, string.Empty);
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
            newTeam.OwnerUserId.Should().Be(_user.Id);
        }

        [Then(@"пользователь '([^']*)' является участником команды '([^']*)'")]
        public async Task ThenПользовательЯвляетсяУчастникомКоманды(string username, string team)
        {
            var newTeam = await Db.Teams.Find(x => x.Name == team).FirstOrDefaultAsync();
            var participants = await Db.TeamParticipants.Find(x => x.TeamId == newTeam.Id).ToListAsync();
            participants.Count.Should().Be(1);
            participants.First().UserId.Should().Be(_user.Id);
        }
    }
}