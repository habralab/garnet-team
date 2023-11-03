using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.Api.TeamEditOwner;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.Team;
using Garnet.Teams.Infrastructure.MongoDb.TeamParticipant;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamEditOwner
{
    [Binding]
    public class TeamEditOwnerStep : BaseSteps
    {
        private QueryExceptionsContext _queryExceptionsContext;
        private CurrentUserProviderFake _currentUserProviderFake;
        private readonly UpdateDefinitionBuilder<TeamDocument> _u = Builders<TeamDocument>.Update;
        public TeamEditOwnerStep(QueryExceptionsContext queryExceptionsContext, CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [Given(@"пользователь '([^']*)' является участником команды '([^']*)'")]
        public async Task ThenПользовательЯвляетсяУчастникомКоманды(string username, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstOrDefaultAsync();
            var userId = _currentUserProviderFake.GetUserIdByUsername(username);
            var participant = TeamParticipantDocument.Create(Uuid.NewMongo(), userId, username, team.Id, null);
            await Db.TeamParticipants.InsertOneAsync(participant);
            await Db.Teams.UpdateOneAsync(
                x => x.Name == teamName,
                _u.Inc(x => x.ParticipantCount, 1)
            );
        }

        [When(@"'([^']*)' изменяет владельца команды '([^']*)' на пользователя '([^']*)'")]
        public async Task WhenИзменяетВладельцаКомандыНаПользователя(string username, string teamName, string newOwnerUsername)
        {
            _currentUserProviderFake.LoginAs(username);
            var newOwnerUserId = _currentUserProviderFake.GetUserIdByUsername(newOwnerUsername);

            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var input = new TeamEditOwnerInput(team.Id, newOwnerUserId);

            try
            {
                await Mutation.TeamEditOwner(CancellationToken.None, input);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }
    }
}