using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestGet;
using Garnet.Projects.Infrastructure.MongoDb;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamJoinRequestGet;

[Binding]
public class ProjectTeamJoinRequestGetSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private ProjectTeamJoinRequestGetPayload? _response;
    private QueryExceptionsContext _errorStepContext = null!;

    private readonly FilterDefinitionBuilder<ProjectDocument> _f =
        Builders<ProjectDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectDocument> _u =
        Builders<ProjectDocument>.Update;

    public ProjectTeamJoinRequestGetSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
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
        var teamJoinRequest = GiveMe.ProjectTeamJoinRequest().WithTeamName(teamName).WithProjectId(project.Id);
        await Db.ProjectTeamJoinRequests.InsertOneAsync(teamJoinRequest);
    }

    [When(@"пользователь '([^']*)' просматривает заявки проекта '([^']*)'")]
    public async Task WhenПользовательПросматриваетЗаявкиНаВступлениеВПроект(string username, string projectName)
    {
        var claims = _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();

        try
        {
            _response = await Query.GetProjectTeamJoinRequestsByProjectId(CancellationToken.None, claims,
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
        _response!.projectTeamJoinRequest.Count().Should().Be(teamJoinRequestCount);
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