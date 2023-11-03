using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectTaskEditTags;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskEditTags;

[Binding]
public class ProjectTaskEditTagsSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    private readonly FilterDefinitionBuilder<ProjectTaskDocument> _f =
        Builders<ProjectTaskDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTaskDocument> _u =
        Builders<ProjectTaskDocument>.Update;

    public ProjectTaskEditTagsSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"теги задачи '(.*)' состоят из '(.*)'")]
    public async Task GivenПользовательРедактируетТегиЗадачи(string taskName, string tags)
    {
        var tagList = tags.Split(", ");
        await Db.ProjectTasks.UpdateOneAsync(
            _f.Eq(x => x.Name, taskName),
            _u.Set(x => x.Tags, tagList));
    }

    [When(@"пользователь '(.*)' редактирует теги задачи с названием '(.*)' на '(.*)'")]
    public async Task WhenПользовательРедактируетТегиЗадачи(string username, string taskName, string tags)
    {
        _currentUserProviderFake.LoginAs(username);
        var tagList = tags.Split(", ");
        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();
        var input = new ProjectTaskEditTagsInput(task.Id, tagList);
        try
        {
            await Mutation.ProjectTaskEditTags(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"и теги задачи '(.*)' состоят из '(.*)'")]
    public async Task ThenВСистемеСуществуетЗадачаСТегами(string taskName, string tags)
    {
        var tagList = tags.Split(", ");
        var task = await Db.ProjectTasks.Find(x => x.Name == taskName).FirstOrDefaultAsync();
        task.Tags.Should().BeEquivalentTo(tagList);
    }
}