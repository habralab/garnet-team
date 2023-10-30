using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectTaskEditName;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskNameEdit;

[Binding]
public class ProjectTaskNameEditSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    public ProjectTaskNameEditSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"существует задача '(.*)' в проекте '(.*)'")]
    public async Task ThenСуществуетЗадачаВПроекте(string taskName, string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var task = GiveMe.ProjectTask().WithName(taskName).WithProjectId(project.Id).Build();
        await Db.ProjectTasks.InsertOneAsync(task);
    }

    [When(@"пользователь '(.*)' редактирует название задачи '(.*)' на '(.*)'")]
    public async Task WhenПользовательРедактируетНазваниеЗадачи(string username, string taskName, string newTaskName)
    {
        _currentUserProviderFake.LoginAs(username);
        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();

        try
        {
            await Mutation.ProjectTaskEditName(CancellationToken.None, task.ProjectId, task.Name, newTaskName);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"в проекте '(.*)' существует задача с названием '(.*)'")]
    public async Task ThenВСистемеСуществуетЗадачаСНазванием(string projectName, string newTaskName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var task = await Db.ProjectTasks.Find(x => x.ProjectId == project.Id & x.Name == newTaskName)
            .FirstOrDefaultAsync();
        task.Name.Should().Be(newTaskName);
    }
}