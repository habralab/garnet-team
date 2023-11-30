using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Project.AcceptanceTests.FakeServices.NotificationFake;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestGet;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamJoinRequestGet;

[Binding]
public class ProjectTeamJoinRequestGetSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private ProjectTeamJoinRequestGetPayload? _response;
    private readonly QueryExceptionsContext _errorStepContext = null!;
    private readonly SendNotificationCommandMessageFakeConsumer _sendNotificationCommandMessageFakeConsumer;

    private readonly FilterDefinitionBuilder<ProjectDocument> _f =
        Builders<ProjectDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectDocument> _u =
        Builders<ProjectDocument>.Update;

    public ProjectTeamJoinRequestGetSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        SendNotificationCommandMessageFakeConsumer sendNotificationCommandMessageFakeConsumer,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
        _sendNotificationCommandMessageFakeConsumer = sendNotificationCommandMessageFakeConsumer;
    }

    [Given(@"пользователь '([^']*)' является владельцем проекта '([^']*)'")]
    public async Task GivenПользовательЯвляетсяВладельцемПроекта(string username, string projectName)
    {
        await Db.Projects.UpdateOneAsync(
            _f.Eq(x => x.ProjectName, projectName),
            _u.Set(x => x.OwnerUserId, _currentUserProviderFake.GetUserIdByUsername(username)));
    }

    [Given(@"существует заявка от команды '([^']*)' на участие в проекте '([^']*)'")]
    public async Task GivenСуществуетЗаявкаОтКомандыНаУчастиеВПроекте(string teamName, string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var team = await Db.ProjectTeams.Find(x => x.TeamName == teamName).FirstAsync();
        
        var teamJoinRequestId = Uuid.NewMongo(); 
        var teamJoinRequest = GiveMe.ProjectTeamJoinRequest().WithTeamId(team.Id).WithTeamName(teamName).WithProjectId(project.Id).WithId(teamJoinRequestId);
        await Db.ProjectTeamJoinRequests.InsertOneAsync(teamJoinRequest);

        _sendNotificationCommandMessageFakeConsumer.Notifications.Add(
            GiveMe.SendNotificationCommandMessage()
                .WithUserId(project.OwnerUserId)
                .WithType("TeamJoinProjectRequest")
                .WithLinkedEntityId(teamJoinRequestId)
                .WithQuotedEntityIds(new[] { team.Id, project.Id })
        );
    }

    [When(@"пользователь '([^']*)' просматривает заявки проекта '([^']*)'")]
    public async Task WhenПользовательПросматриваетЗаявкиНаВступлениеВПроект(string username, string projectName)
    {
        _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();

        try
        {
            _response = await Query.GetProjectTeamJoinRequestsByProjectId(CancellationToken.None,
                new ProjectTeamJoinRequestGetInput(project.Id));
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }

    }

    [Then(@"в списке заявок отображается '([^']*)' команды")]
    public Task ThenВСпискеЗаявокОтображаетсяКоманд(int teamJoinRequestCount)
    {
        _response!.ProjectTeamJoinRequest.Count().Should().Be(teamJoinRequestCount);
        return Task.CompletedTask;
    }

    [Then(@"происходит ошибка '([^']*)'")]
    public Task ThenПроисходитОшибка(string errorMsg)
    {
        var validError = _errorStepContext.QueryExceptions.First().Errors.Any(x => x.Code == errorMsg);
        validError.Should().BeTrue();
        return Task.CompletedTask;
    }
}