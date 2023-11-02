using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectEditTags;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTagsEdit;

[Binding]
public class ProjectTagsEditSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext = null!;
    private ProjectEditTagsPayload? _response;

    private readonly FilterDefinitionBuilder<ProjectDocument> _f =
        Builders<ProjectDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectDocument> _u =
        Builders<ProjectDocument>.Update;

    public ProjectTagsEditSteps(QueryExceptionsContext errorStepContext,
        CurrentUserProviderFake currentUserProviderFake,
        StepsArgs args) : base(args)
    {
        _errorStepContext = errorStepContext;
        _currentUserProviderFake = currentUserProviderFake;
    }


    [Given(@"теги проекта '(.*)' состоят из '(.*)'")]
    public async Task GivenТегиПроектаСостоятИз(string projectName, string tags)
    {
        var tagList = tags.Split(", ");
        await Db.Projects.FindOneAndUpdateAsync(
            _f.Eq(x => x.ProjectName, projectName),
            _u.Set(x => x.Tags, tagList));
    }

    [When(@"'(.*)' редактирует теги проекта '(.*)' на '(.*)'")]
    public async Task WhenПользовательРедактируетТегиПроекта(string username, string projectName, string tags)
    {
        var tagList = tags.Split(", ");
        _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        var input = new ProjectEditTagsInput(project.Id, tagList);

        try
        {
            _response = await Mutation.ProjectEditTags(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"теги проекта '(.*)' состоят из '(.*)'")]
    public async Task ThenТегиПроектаСостоятИз(string projectName, string tags)
    {
        var tagList = tags.Split(", ");
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        project.Tags.Should().BeEquivalentTo(tagList);
    }
}