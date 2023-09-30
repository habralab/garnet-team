using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.Api.TeamDelete;
using Garnet.Teams.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamDelete
{
    [Binding]
    public class TeamDeleteSteps : BaseSteps
    {
        private readonly FilterDefinitionBuilder<TeamDocument> _f = Builders<TeamDocument>.Filter;
        private readonly UpdateDefinitionBuilder<TeamDocument> _u = Builders<TeamDocument>.Update;
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private TeamDeletePayload _result = null!;
        private Exception _exception = null!;

        public TeamDeleteSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"владельцем команды '([^']*)' является '([^']*)'")]
        public async Task GivenВладелецемКомандыЯвляется(string teamName, string username)
        {
            _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.FindOneAndUpdateAsync(
                _f.Eq(x => x.Name, teamName),
                _u.Set(o => o.OwnerUserId, _currentUserProviderFake.UserId)
            );

            await Db.TeamParticipants.InsertOneAsync(
                GiveMe.TeamParticipant()
                    .WithTeamId(team.Id)
                    .WithUserId(_currentUserProviderFake.UserId)
            );
        }

        [When(@"'([^']*)' удаляет команду '([^']*)'")]
        public async Task WhenУдаляетКоманду(string username, string teamName)
        {
            var claims = _currentUserProviderFake.LoginAs(username);
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            try
            {
                _result = await Mutation.TeamDelete(CancellationToken.None, claims, team.Id);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"команды '([^']*)' в системе не существует")]
        public async Task ThenКомандыВСистемеНеСуществует(string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstOrDefaultAsync();
            team.Should().BeNull();
        }

        [Then(@"'([^']*)' не является участником команды")]
        public async Task ThenНеЯвляетсяУчастникомКоманды(string username)
        {
            _currentUserProviderFake.LoginAs(username);
            var participants = await Db.TeamParticipants.Find(x=> x.UserId == _currentUserProviderFake.UserId).ToListAsync();
            participants.Should().BeEmpty();
        }

        [Then(@"пользователь получает ошибку '([^']*)'")]
        public Task ThenПользовательПолучаетОшибку(string errorMsg)
        {
            _exception.Message.Should().Be(errorMsg);
            return Task.CompletedTask;
        }
    }
}