using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectDelete;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectDelete;

[Binding]
public class ProjectDeleteSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private ProjectDeletePayload? _response;
    private QueryExceptionsContext _errorStepContext;

    public ProjectDeleteSteps(CurrentUserProviderFake currentUserProviderFake, QueryExceptionsContext errorStepContext,
        StepsArgs args) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }


    [Given(@"существует проект '(.*)' с владельцем '(.*)'")]
    public async Task ThenСуществуетПроектСНазваниемИВладельцем(string projectName, string username)
    {
        _currentUserProviderFake.LoginAs(username);
        var project = GiveMe.Project().WithProjectName(projectName).WithOwnerUserId(_currentUserProviderFake.UserId);
        await Db.Projects.InsertOneAsync(project);
    }

    [When(@"'(.*)' удаляет проект '(.*)'")]
    public async Task WhenПользовательУдаляетПроект(string username, string projectName)
    {
        var claims = _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();

        try
        {
            _response = await Mutation.ProjectDelete(CancellationToken.None, claims, project.Id);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"проекта '(.*)' в системе не существует")]
    public async Task ThenПроектаНеСуществует(string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstOrDefaultAsync();
        project.Should().BeNull();
    }

    [Then(@"пользователь получает ошибку, что '(.*)'")]
    public Task ThenПользовательПолучаетОшибку(string errorMsg)
    {
        var validError = _errorStepContext.QueryExceptions.First().Errors.Any(x=> x.Message == errorMsg);
        validError.Should().BeTrue();
        return Task.CompletedTask;
    }

    [Then(@"в системе присутствует проект '(.*)'")]
    public async Task ThenПроектСуществуетВСистеме(string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstOrDefaultAsync();
        project.Should().NotBeNull();
    }
}