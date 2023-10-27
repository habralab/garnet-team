using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectGet;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectGet;

[Binding]
public class ProjectGetSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private ProjectPayload? _response;
    private readonly DateTimeServiceFake _dateTimeServiceFake;

    public ProjectGetSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args,
        DateTimeServiceFake dateTimeServiceFake) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _dateTimeServiceFake = dateTimeServiceFake;
    }


    [Given(@"существует проект '(.*)' с описанием '(.*)'")]
    public async Task ThenСуществуетПроектСНазваниемИОписанием(string projectName, string description)
    {
        var audit = AuditInfoDocument.Create(_dateTimeServiceFake.UtcNow, _currentUserProviderFake.UserId);
        var project = GiveMe.Project().WithProjectName(projectName).WithDescription(description).Build();

        project = project with { AuditInfo = audit };

        await Db.Projects.InsertOneAsync(project);
    }

    [When(@"пользователь '(.*)' просматривает карточку проекта '(.*)'")]
    public async Task WhenПользовательПросматриваетПроект(string username, string projectName)
    {
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        _response = await Query.ProjectGet(CancellationToken.None, project.Id);
    }

    [Then(@"описание проекта состоит из '(.*)'")]
    public Task ThenОписаниеПроектаСостоитИз(string description)
    {
        _response!.Description.Should().Be(description);
        return Task.CompletedTask;
    }
}