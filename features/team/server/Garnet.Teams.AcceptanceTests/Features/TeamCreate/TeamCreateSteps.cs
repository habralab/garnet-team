using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamCreate;
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
            newTeam.OwnerUserId.Should().Be(_currentUserProviderFake.UserId);
        }

        [Then(@"пользователь '([^']*)' является участником команды '([^']*)'")]
        public async Task ThenПользовательЯвляетсяУчастникомКоманды(string username, string team)
        {
            var newTeam = await Db.Teams.Find(x => x.Name == team).FirstOrDefaultAsync();
            var participants = await Db.TeamParticipants.Find(x => x.TeamId == newTeam.Id).ToListAsync();
            participants.Count.Should().Be(1);
            participants.First().UserId.Should().Be(_currentUserProviderFake.UserId);
        }
    }
}