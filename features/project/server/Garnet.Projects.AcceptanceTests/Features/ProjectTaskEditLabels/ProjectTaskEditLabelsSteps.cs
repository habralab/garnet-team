using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectTaskEditLabels;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskEditLabels;

[Binding]
public class ProjectTaskEditLabelsSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    private readonly FilterDefinitionBuilder<ProjectTaskDocument> _f =
        Builders<ProjectTaskDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTaskDocument> _u =
        Builders<ProjectTaskDocument>.Update;

    public ProjectTaskEditLabelsSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"лейблы задачи '(.*)' состоят из '(.*)'")]
    public async Task GivenПользовательРедактируетЛейбламиЗадачи(string taskName, string labels)
    {
        var labelList = labels.Split(", ");
        await Db.ProjectTasks.UpdateOneAsync(
            _f.Eq(x => x.Name, taskName),
            _u.Set(x => x.Labels, labelList));
    }

    [When(@"пользователь '(.*)' редактирует лейблы задачи с названием '(.*)' на '(.*)'")]
    public async Task WhenПользовательРедактируетЛейбламиЗадачи(string username, string taskName, string labels)
    {
        _currentUserProviderFake.LoginAs(username);
        var labelList = labels.Split(", ");
        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();
        var input = new ProjectTaskEditLabelsInput(task.Id, labelList);
        try
        {
            await Mutation.ProjectTaskEditLabels(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"и лейблы задачи '(.*)' состоят из '(.*)'")]
    public async Task ThenВСистемеСуществуетЗадачаСЛейблами(string taskName, string labels)
    {
        var labelList = labels.Split(", ");
        var task = await Db.ProjectTasks.Find(x => x.Name == taskName).FirstOrDefaultAsync();
        task.Labels.Should().BeEquivalentTo(labelList);
    }
}