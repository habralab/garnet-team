using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectTaskDelete;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskDelete;

[Binding]
public class ProjectTaskDeleteSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;
    private ProjectTaskDeletePayload? _response;

    private readonly FilterDefinitionBuilder<ProjectTeamParticipantDocument> _f =
        Builders<ProjectTeamParticipantDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTeamParticipantDocument> _u =
        Builders<ProjectTeamParticipantDocument>.Update;

    public ProjectTaskDeleteSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [When(@"пользователь '(.*)' удаляет из проекта '(.*)' задачу с названием '(.*)'")]
    public async Task WhenПользовательУдаляетИзПроектаЗадачу(string username, string projectName, string taskName)
    {
        _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        var task = await Db.ProjectTasks.Find(x => x.ProjectId == project.Id & x.Name == taskName).FirstAsync();
        try
        {
            _response = await Mutation.ProjectTaskDelete(CancellationToken.None, task.Id);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"в системе не существует задачи с названием '(.*)'")]
    public async Task ThenВСистемеНеСуществуетЗадачи(string taskName)
    {
        var task = await Db.ProjectTasks.Find(x => x.Name == taskName).FirstOrDefaultAsync();
        task.Should().BeNull();
    }
}