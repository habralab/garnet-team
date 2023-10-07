using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestApprove;
using MongoDB.Driver;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestApprove
{
    [Binding]
    public class TeamUserJoinRequestDecideSteps : BaseSteps
    {
        private CurrentUserProviderFake _currentUserProviderFake;

        public TeamUserJoinRequestDecideSteps(
            CurrentUserProviderFake currentUserProviderFake,
            StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        private async Task<TeamUserJoinRequestDecideInput> SetJoinRequestDecision(string teamName, string username, bool decisition)
        {
            var user = await Db.TeamUsers.Find(x => x.Username == username).FirstAsync();
            var team = await Db.Teams.Find(x => x.Name == teamName).FirstAsync();
            var userJoinRequest = await Db.TeamUserJoinRequest.Find(x => x.UserId == user.Id & x.TeamId == team.Id).FirstAsync();

            return new TeamUserJoinRequestDecideInput(userJoinRequest.Id, decisition);
        }

        [When(@"'(.*)' принимает заявку на вступление в команду '(.*)' от пользователя '(.*)'")]
        public async Task WhenПринимаетЗаявкуНаВступлениеВКомандуОтПользоваьтеля(string ownerUsername, string teamName, string username)
        {
            var input = await SetJoinRequestDecision(teamName, username, true);
            var claims = _currentUserProviderFake.LoginAs(ownerUsername);

            await Mutation.TeamUserJoinRequestDecide(CancellationToken.None, claims, input);
        }

        [When(@"'(.*)' отклоняет заявку на вступление в команду '(.*)' от пользователя '(.*)'")]
        public async Task WhenОтклоняетЗаявкуНаВступлениеВКомандуОтПользоваьтеля(string ownerUsername, string teamName, string username)
        {
            var input = await SetJoinRequestDecision(teamName, username, false);
            var claims = _currentUserProviderFake.LoginAs(ownerUsername);

            await Mutation.TeamUserJoinRequestDecide(CancellationToken.None, claims, input);
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