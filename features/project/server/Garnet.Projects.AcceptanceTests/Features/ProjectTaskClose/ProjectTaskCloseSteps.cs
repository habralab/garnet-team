using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskClose;

[Binding]
public class ProjectTaskCloseSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    private readonly FilterDefinitionBuilder<ProjectTaskDocument> _f =
        Builders<ProjectTaskDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTaskDocument> _u =
        Builders<ProjectTaskDocument>.Update;

    public ProjectTaskCloseSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"пользователь '(.*)' является отвественным по задаче '(.*)'")]
    public async Task GivenПользовательЯвляетсяОтвественнымПоЗадаче(string username, string taskName)
    {
        var user = await Db.ProjectUsers.Find(x => x.UserName == username).FirstAsync();
        await Db.ProjectTasks.UpdateOneAsync(
            _f.Eq(x => x.Name, taskName),
            _u.Set(x => x.ResponsibleUserId, user.Id));
    }

    [When(@"пользователь '(.*)' закрывает задачу с названием '(.*)'")]
    public async Task WhenПользовательЗакрываетЗадачу(string username, string taskName)
    {
        _currentUserProviderFake.LoginAs(username);
        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();

        try
        {
            await Mutation.ProjectTaskClose(CancellationToken.None, task.Id);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"в системе у задачи с названием '(.*)' установлен статус '(.*)'")]
    public async Task ThenУЗадачиУстановленСтатус(string taskName, string status)
    {
        var task = await Db.ProjectTasks.Find(x => x.Name == taskName)
            .FirstOrDefaultAsync();
        task.Status.Should().Be(status);
    }
}