using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamEditDescription;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamEditDescription
{
    [Binding]
    public class TeamEditSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _errorStepContext = null!;

        public TeamEditSteps(QueryExceptionsContext errorStepContext, CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _errorStepContext = errorStepContext;
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"'([^']*)' редактирует описание карточки команды '([^']*)' на '([^']*)'")]
        public async Task WhenРедактируетОписаниеКарточкиКоманды(string username, string teamName, string description)
        {
            _currentUserProviderFake.LoginAs(username);

            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var input = new TeamEditDescriptionInput(team.Id, description);

            try
            {
                await Mutation.TeamEditDescription(CancellationToken.None, input);
            }
            catch (QueryException ex)
            {
                _errorStepContext.QueryExceptions.Add(ex);
            }
        }

        [Then(@"описание команды '([^']*)' состоит из '([^']*)'")]
        public async Task ThenОписаниеКомандыСостоитИз(string teamName, string description)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            team.Description.Should().Be(description);
        }
    }
}