using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectFilter;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectFilter;

[Binding]
public class ProjectFilterSteps : BaseSteps
{
    private ProjectFilterPayload? _response;
    private readonly DateTimeServiceFake _dateTimeServiceFake;
    private readonly CurrentUserProviderFake _currentUserProviderFake;


    public ProjectFilterSteps(StepsArgs args, DateTimeServiceFake dateTimeServiceFake,
        CurrentUserProviderFake currentUserProviderFake) : base(args)
    {
        _dateTimeServiceFake = dateTimeServiceFake;
        _currentUserProviderFake = currentUserProviderFake;
    }


    [Given(@"существует проект '([^']*)'")]
    public async Task GivenСуществуетПроект(string projectName)
    {
        var audit = AuditInfoDocument.Create(_dateTimeServiceFake.UtcNow, _currentUserProviderFake.UserId);
        var user = ProjectUserDocument.Create(Uuid.NewMongo(), "username", null!);
        var project = GiveMe.Project().WithProjectName(projectName).WithOwnerUserId(user.Id).Build();

        project = project with { AuditInfo = audit };

        await Db.Projects.InsertOneAsync(project);
    }

    [Given(@"существует проект '([^']*)' с тегами '([^']*)'")]
    public async Task GivenСуществуетПроектСТегами(string projectName, string tags)
    {
        var user = ProjectUserDocument.Create(Uuid.NewMongo(), "username", null!);
        var tagList = tags.Split(", ");

        var audit = AuditInfoDocument.Create(_dateTimeServiceFake.UtcNow, _currentUserProviderFake.UserId);
        var project = GiveMe.Project().WithProjectName(projectName).WithOwnerUserId(user.Id).WithTags(tagList).Build();

        project = project with { AuditInfo = audit };

        await Db.Projects.InsertOneAsync(project);
    }

    [When(@"производится поиск проектов по запросу '([^']*)'")]
    public async Task WhenПроизводитсяПоискПроектаПоЗапросу(string query)
    {
        _response = await Query.ProjectsFilter(CancellationToken.None, new ProjectFilterInput(query, null, 0, 10));
    }

    [When(@"производится поиск проектов по тегу '([^']*)'")]
    public async Task WhenПроизводитсяПоискПроектаПоТегу(string tags)
    {
        var tagList = tags.Split(", ");
        _response = await Query.ProjectsFilter(CancellationToken.None, new ProjectFilterInput(null, tagList, 0, 10));
    }

    [Then(@"в списке отображается '([^']*)' проект")]
    public Task ThenВСпискеОтображаетсяПроект(int projectCount)
    {
        _response!.Projects.Count().Should().Be(projectCount);
        return Task.CompletedTask;
    }
}