using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.Contexts;
using Garnet.Teams.Infrastructure.Api.TeamEdit;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamEdit
{
    [Binding]
    public class TeamEditSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private ErrorStepContext _errorStepContext = null!;
        private TeamEditPayload _teamEditPayload = null!;

        public TeamEditSteps(ErrorStepContext errorStepContext, CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _errorStepContext = errorStepContext;
            _currentUserProviderFake = currentUserProviderFake;
        }

        [When(@"'([^']*)' редактирует описание карточки команды '([^']*)' на '([^']*)'")]
        public async Task WhenРедактируетОписаниеКарточкиКоманды(string username, string teamName, string description)
        {
            var claims = _currentUserProviderFake.LoginAs(username);

            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var input = new TeamEditInput(team.Id, description);

            try
            {
                _teamEditPayload = await Mutation.TeamEditDescription(CancellationToken.None, claims, input);
            }
            catch (QueryException ex)
            {
                _errorStepContext.QueryException = ex;
            }
        }

        [Scope(Feature = "TeamEdit")]
        [Then(@"описание команды в карточке состоит из '([^']*)'")]
        public Task ThenОписаниеКомандыВКарточкеСостоитИз(string description)
        {
            _teamEditPayload.Description.Should().Be(description);
            return Task.CompletedTask;
        }

        [Then(@"описание команды '([^']*)' состоит из '([^']*)'")]
        public async Task ThenОписаниеКомандыСостоитИз(string teamName, string description)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            team.Description.Should().Be(description);
        }
    }
}