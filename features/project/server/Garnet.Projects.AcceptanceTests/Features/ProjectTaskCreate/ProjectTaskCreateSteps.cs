using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectTaskCreate;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskCreate;

[Binding]
public class ProjectTaskCreateSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;
    private ProjectTaskCreatePayload? _response;

    private readonly FilterDefinitionBuilder<ProjectTeamParticipantDocument> _f =
        Builders<ProjectTeamParticipantDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTeamParticipantDocument> _u =
        Builders<ProjectTeamParticipantDocument>.Update;

    public ProjectTaskCreateSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"пользователь '(.*)' является участником команды '(.*)'")]
    public async Task GivenПользовательЯвляетсяУчастникомКоманды(string username, string teamName)
    {
        var user = await Db.ProjectUsers.Find(x => x.Id == _currentUserProviderFake.GetUserIdByUsername(username))
            .FirstAsync();
        await Db.ProjectTeamsParticipants.UpdateManyAsync(
            _f.Eq(x => x.TeamName, teamName),
            _u.AddToSet(x => x.UserParticipants, user)
        );
    }

    [When(@"пользователь '(.*)' создает в проекте '(.*)' задачу с названием '(.*)'")]
    public async Task WhenПользовательСоздаетВПроектеЗадачу(string username, string projectName, string taskName)
    {
        _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        var input = new ProjectTaskCreateInput(
            project.Id,
            taskName,
            null,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<string>());

        try
        {
            _response = await Mutation.ProjectTaskCreate(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"в системе существует задача с названием '([^']*)'")]
    public async Task ThenВСистемеСуществуетЗадача(string taskName)
    {
        var task = await Db.ProjectTasks.Find(x => x.Name == taskName).FirstOrDefaultAsync();
        task.Should().NotBeNull();
    }
}