using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.MongoDb;
using TechTalk.SpecFlow.Assist;

namespace Garnet.Teams.AcceptanceTests.Features.TeamUserJoinRequestShow
{
    [Binding]
    public class TeamUserJoinRequestsShowSteps : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public TeamUserJoinRequestsShowSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"существуют пользователи и команда со следующими параметрами")]
        public async Task GivenСуществуютПользователиИКомандаСоСледующимиПараметрами(Table table)
        {
            var given = table.CreateInstance<GivenTeamUserJoinRequestsShow>();

            var teamOwner = TeamUserDocument.Create(Uuid.NewMongo(), given.TeamOwner);
            await Db.TeamUsers.InsertOneAsync(teamOwner);
            _currentUserProviderFake.RegisterUser(teamOwner.Username, teamOwner.Id);

            var teamId = Uuid.NewMongo();
            var team = GiveMe.Team()
                .WithId(teamId)
                .WithName(given.TeamName)
                .WithOwnerUserId(teamOwner.Id);
            await Db.Teams.InsertOneAsync(team);

            var requestedToJoinUser = TeamUserDocument.Create(Uuid.NewMongo(), given.RequestedToJoinUser);
            await Db.TeamUsers.InsertOneAsync(requestedToJoinUser);

            var requestToJoin = TeamUserJoinRequestDocument.Create(Uuid.NewMongo(), requestedToJoinUser.Id, teamId);
            await Db.TeamUserJoinRequest.InsertOneAsync(requestToJoin);
        }

        [When(@"'(.*)' просматривает заявки на вступление в команду '(.*)'")]
        public Task WhenПросматриваетЗаявкиНаВступлениеВКоманду(string username, string teamName)
        {
            return Task.CompletedTask;
        }

        [Then(@"в списке отображается '(.*)' заявка")]
        public Task ThenВСпискеОтображаетсяКоманда(int resultCount)
        {
            return Task.CompletedTask;
        }
    }
}