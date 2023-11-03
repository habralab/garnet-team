using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskOpen;

[Binding]
public class ProjectTaskOpenSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    private readonly FilterDefinitionBuilder<ProjectTaskDocument> _f =
        Builders<ProjectTaskDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTaskDocument> _u =
        Builders<ProjectTaskDocument>.Update;

    public ProjectTaskOpenSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"задача '(.*)' имеет статус '(.*)'")]
    public async Task GivenЗадачаИмеетСтатус(string taskName, string status)
    {
        await Db.ProjectTasks.UpdateOneAsync(
            _f.Eq(x => x.Name, taskName),
            _u.Set(x => x.Status, status));
    }

    [When(@"пользователь '(.*)' открывает задачу с названием '(.*)'")]
    public async Task WhenПользовательОткрываетЗадачу(string username, string taskName)
    {
        _currentUserProviderFake.LoginAs(username);
        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();

        try
        {
            await Mutation.ProjectTaskOpen(CancellationToken.None, task.Id);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }
}