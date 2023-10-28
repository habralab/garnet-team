using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectDelete;
using Garnet.Projects.Infrastructure.MongoDb.Project;
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
    private readonly DateTimeServiceFake _dateTimeServiceFake;

    public ProjectDeleteSteps(CurrentUserProviderFake currentUserProviderFake, QueryExceptionsContext errorStepContext,
        StepsArgs args, DateTimeServiceFake dateTimeServiceFake) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
        _dateTimeServiceFake = dateTimeServiceFake;
    }


    [Given(@"существует проект '([^']*)' с владельцем '([^']*)'")]
    public async Task GivenСуществуетПроектСНазваниемИВладельцем(string projectName, string username)
    {
        var project = GiveMe.Project().WithProjectName(projectName)
            .WithOwnerUserId(_currentUserProviderFake.GetUserIdByUsername(username)).Build();

        await Db.Projects.InsertOneAsync(project);
    }

    [When(@"'(.*)' удаляет проект '(.*)'")]
    public async Task WhenПользовательУдаляетПроект(string username, string projectName)
    {
        _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();

        try
        {
            _response = await Mutation.ProjectDelete(CancellationToken.None, project.Id);
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


    [Then(@"в системе присутствует проект '(.*)'")]
    public async Task ThenПроектСуществуетВСистеме(string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstOrDefaultAsync();
        project.Should().NotBeNull();
    }
}