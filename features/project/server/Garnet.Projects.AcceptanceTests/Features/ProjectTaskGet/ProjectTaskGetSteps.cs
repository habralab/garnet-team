using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectTaskGet;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskGet;

[Binding]
public class ProjectTaskGetSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;
    private ProjectTaskGetPayload? _response;

    public ProjectTaskGetSteps(StepsArgs args,
        CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"в проекте '(.*)' существует задача '(.*)'")]
    public async Task ThenВПроектеСуществуетЗадача(string projectName, string taskName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstOrDefaultAsync();
        var task = GiveMe.ProjectTask().WithName(taskName).WithProjectId(project.Id);
        await Db.ProjectTasks.InsertOneAsync(task);
    }

    [When(@"пользователь '(.*)' открывает в проекте '(.*)' задачу с названием '(.*)'")]
    public async Task WhenПользовательСоздаетВПроектеЗадачу(string username, string projectName, string taskName)
    {
        _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        var task = await Db.ProjectTasks.Find(o => o.ProjectId == project.Id & o.Name == taskName).FirstAsync();
        try
        {
            _response = await Query.ProjectTaskGetById(CancellationToken.None, task.Id);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"название задачи состоит из '(.*)'")]
    public Task ThenВСистемеСуществуетЗадача(string taskName)
    {
        _response!.Name.Should().Be(taskName);
        return Task.CompletedTask;
    }
}