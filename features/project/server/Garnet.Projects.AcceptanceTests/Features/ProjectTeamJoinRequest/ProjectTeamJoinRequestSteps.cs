using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.EventHandlers.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeam;
using Garnet.Teams.Events.TeamJoinProjectRequest;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamJoinRequest;

[Binding]
public class ProjectTeamJoinRequestSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private readonly ProjectTeamJoinRequestCreatedConsumer _projectTeamJoinRequestCreatedConsumer;


    private readonly FilterDefinitionBuilder<ProjectTeamDocument> _f =
        Builders<ProjectTeamDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTeamDocument> _u =
        Builders<ProjectTeamDocument>.Update;


    public ProjectTeamJoinRequestSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        ProjectTeamJoinRequestCreatedConsumer projectTeamJoinRequestCreatedConsumer) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _projectTeamJoinRequestCreatedConsumer = projectTeamJoinRequestCreatedConsumer;
    }

    [Given(@"пользователь '([^']*)' является владельцем команды '([^']*)'")]
    public async Task GivenПользовательЯвляетсяВладельцемКоманды(string username, string teamName)
    {
        await Db.ProjectTeams.FindOneAndUpdateAsync(
            _f.Eq(x => x.TeamName, teamName),
            _u.Set(x => x.OwnerUserId, _currentUserProviderFake.GetUserIdByUsername(username))
                .AddToSet(x => x.UserParticipantId, _currentUserProviderFake.GetUserIdByUsername(username))
        );
    }

    [When(@"пользователь '([^']*)' отправляет заявку на вступление в проект '([^']*)' от лица команды '([^']*)'")]
    public async Task WhenПользовательОтправляетЗаявкуНаВступлениеВПроект(string username, string projectName,
        string teamName)
    {
        _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var team = await Db.ProjectTeams.Find(x => x.TeamName == teamName).FirstAsync();
        await _projectTeamJoinRequestCreatedConsumer.Consume(
            new TeamJoinProjectRequestCreatedEvent(Uuid.NewMongo(), project.Id, team.Id));
    }

    [Then(@"в проекте '([^']*)' существует заявка на вступление от команды '([^']*)'")]
    public async Task ThenВПроектеСуществуетЗаявкаНаВступлениеОтКоманды(string projectName, string teamName)
    {
        var teamJoinRequest = await Db.ProjectTeamJoinRequests.Find(x => x.TeamName == teamName).FirstAsync();
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        teamJoinRequest.ProjectId.Should().Be(project.Id);
    }
}