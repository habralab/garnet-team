using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;
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
            var input = new TeamJoinInvitePayload(userId, team.Id);

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
        public Task ThenУПользователяКоличествоПриглашенийВКомандыРавно(string username, int joinInviteCount)
        {
            return Task.CompletedTask;
        }

        [Given(@"существует приглашение пользователя '(.*)' на вступление в команду '(.*)' от владельца")]
        public Task GivenСущесвуетПриглашениеПользователяНаВступлениеВКомандуОтВладельца(string username, string teamName)
        {
            return Task.CompletedTask;
        }
    }
}