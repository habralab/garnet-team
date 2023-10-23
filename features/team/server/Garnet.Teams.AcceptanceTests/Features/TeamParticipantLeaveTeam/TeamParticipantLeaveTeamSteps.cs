using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamParticipantLeaveTeam
{
    [Binding]
    public class TeamParticipantLeaveTeamSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _queryExceptionsContext;
        public TeamParticipantLeaveTeamSteps(
            QueryExceptionsContext queryExceptionsContext,
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
            _queryExceptionsContext = queryExceptionsContext;
        }

        [When(@"'(.*)' выходит из состава команды '(.*)'")]
        public async Task WhenВыходитИзСоставаКоманды(string username, string teamName)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();

            _currentUserProviderFake.LoginAs(username);
            try
            {
                await Mutation.TeamParticipantLeaveTeam(CancellationToken.None, team.Id);
            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }
    }
}