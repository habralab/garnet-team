using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskEditResponsibleUser;

[Binding]
public class ProjectTaskEditResponsibleUserSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    private readonly FilterDefinitionBuilder<ProjectTaskDocument> _f =
        Builders<ProjectTaskDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTaskDocument> _u =
        Builders<ProjectTaskDocument>.Update;

    public ProjectTaskEditResponsibleUserSteps(StepsArgs args,
        CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [When(@"пользователь '(.*)' меняет ответственного по задаче '(.*)' на пользователя '(.*)'")]
    public async Task WhenПользовательМеняетОтветтственногоЗадачи(string username, string taskName, string newResponsibleUserName)
    {
        _currentUserProviderFake.LoginAs(username);
        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();
        var user = await Db.ProjectUsers.Find(o => o.UserName == newResponsibleUserName).FirstAsync();

        try
        {
            await Mutation.ProjectTaskEditResponsibleUser(CancellationToken.None, task.Id, user.Id);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"ответственным по задаче '(.*)' является пользователь '(.*)'")]
    public async Task ThenОтветственнымЗадачиЯвляется(string taskName, string responsibleUserName)
    {
        var user = await Db.ProjectUsers.Find(x => x.UserName == responsibleUserName).FirstOrDefaultAsync();
        var task = await Db.ProjectTasks.Find(x => x.Name == taskName).FirstOrDefaultAsync();
        task.ResponsibleUserId.Should().BeEquivalentTo(user.Id);
    }
}