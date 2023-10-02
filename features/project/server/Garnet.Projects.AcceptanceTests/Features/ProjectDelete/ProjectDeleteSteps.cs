using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectDelete;
using Garnet.Projects.Infrastructure.MongoDb;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectGet;

[Binding]
public class ProjectDeleteSteps : BaseSteps
{
    private readonly FilterDefinitionBuilder<ProjectDocument> _f = Builders<ProjectDocument>.Filter;
    private readonly UpdateDefinitionBuilder<ProjectDocument> _u = Builders<ProjectDocument>.Update;
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private ProjectDeletePayload _response;
    private QueryExceptionsContext _errorStepContext;

    public ProjectDeleteSteps(CurrentUserProviderFake currentUserProviderFake, QueryExceptionsContext errorStepContext,
        StepsArgs args) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }


    [Given(@"существует проект '(.*)' с владельцем '(.*)'")]
    public async Task ThenСуществуетПроектСНазваниемИВладельцем(string projectName, string ownerUserId)
    {
        var project = GiveMe.Project().WithProjectName(projectName).WithOwnerUserId(ownerUserId);
        await Db.Projects.InsertOneAsync(project);
    }

    [When(@"'(.*)' удаляет проект '(.*)'")]
    public async Task WhenПользовательУдаляетПроект(string username, string projectName)
    {
        var claims = _currentUserProviderFake.LoginAs(username);
        var team = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();

        try
        {
            _response = await Mutation.ProjectDelete(CancellationToken.None, claims, team.Id);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"проекта '(.*)' в системе не существует")]
    public async Task ThenПроектаНеСуществует(string projectName)
    {
    }

    [Then(@"пользователь получает ошибку, что '(.*)'")]
    public async Task ThenПользовательПолучаетОшибку(string errorMsg)
    {
    }

    [Then(@"в системе присутствует проект '(.*)'")]
    public async Task ThenПроектСуществуетВСистеме(string projectName)
    {
    }
}