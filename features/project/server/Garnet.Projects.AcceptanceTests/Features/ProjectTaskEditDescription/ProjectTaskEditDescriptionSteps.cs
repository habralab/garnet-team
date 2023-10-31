using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.AcceptanceTests.Support;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskEditDescription;

[Binding]
public class ProjectTaskEditDescriptionSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    public ProjectTaskEditDescriptionSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"в проекте '([^']*)' существует задача '([^']*)'  с описанием '([^']*)'")]
    public async Task ThenВПроектеСуществуетЗадачаСОписанием(string projectName, string taskName, string description)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var task = GiveMe.ProjectTask().WithName(taskName).WithProjectId(project.Id).WithDescription(description)
            .Build();
        await Db.ProjectTasks.InsertOneAsync(task);
    }

    [When(@"пользователь '(.*)' редактирует описание задачи с названием '(.*)' на '(.*)'")]
    public async Task WhenПользовательРедактируетОписаниеЗадачи(string username, string taskName, string description)
    {
        _currentUserProviderFake.LoginAs(username);
        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();

        try
        {
            await Mutation.ProjectTaskEditDescription(CancellationToken.None, task.Id, description);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"в проекте '(.*)' существует задача '(.*)' с описанием '(.*)'")]
    public async Task ThenВСистемеСуществуетЗадачаСОписанием(string projectName, string taskName, string description)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var task = await Db.ProjectTasks.Find(x => x.ProjectId == project.Id & x.Name == taskName)
            .FirstOrDefaultAsync();
        task.Description.Should().Be(description);
    }
}