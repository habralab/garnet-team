using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestDecide;
using HotChocolate.Execution;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestDecide
{
    [Binding]
    public class TeamUserJoinRequestDecideSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;
        private readonly QueryExceptionsContext _queryExceptionsContext;

        public TeamUserJoinRequestDecideSteps(
            CurrentUserProviderFake currentUserProviderFake,
            QueryExceptionsContext queryExceptionsContext,
            StepsArgs args) : base(args)
        {
            _queryExceptionsContext = queryExceptionsContext;
            _currentUserProviderFake = currentUserProviderFake;
        }

        private async Task<TeamUserJoinRequestDecideInput> SetJoinRequestDecision(string teamName, string username, bool decisition)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var userJoinRequest = await Db.TeamUserJoinRequests.Find(x => x.UserId == user.Id & x.TeamId == team.Id).FirstAsync();

            return new TeamUserJoinRequestDecideInput(userJoinRequest.Id, decisition);
        }

        [When(@"'(.*)' принимает заявку на вступление в команду '(.*)' от пользователя '(.*)'")]
        public async Task WhenПринимаетЗаявкуНаВступлениеВКомандуОтПользоваьтеля(string ownerUsername, string teamName, string username)
        {
            var input = await SetJoinRequestDecision(teamName, username, true);
            _currentUserProviderFake.LoginAs(ownerUsername);

            try
            {
                await Mutation.TeamUserJoinRequestDecide(CancellationToken.None, input);

            }
            catch (QueryException ex)
            {
                _queryExceptionsContext.QueryExceptions.Add(ex);
            }
        }

        [When(@"'(.*)' отклоняет заявку на вступление в команду '(.*)' от пользователя '(.*)'")]
        public async Task WhenОтклоняетЗаявкуНаВступлениеВКомандуОтПользоваьтеля(string ownerUsername, string teamName, string username)
        {
            var input = await SetJoinRequestDecision(teamName, username, false);
            _currentUserProviderFake.LoginAs(ownerUsername);

            await Mutation.TeamUserJoinRequestDecide(CancellationToken.None, input);
        }

        [Then(@"в команде '(.*)' количество участников равно '(.*)'")]
        public async Task ThenВКомандеКоличествоУчастниковРавно(string teamName, int participantCount)
        {
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var participants = await Db.TeamParticipants.Find(x => x.TeamId == team.Id).ToListAsync();
            participants.Count.Should().Be(participantCount);
        }
    }
}