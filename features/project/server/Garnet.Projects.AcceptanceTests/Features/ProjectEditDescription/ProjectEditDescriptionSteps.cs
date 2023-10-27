using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectEditDescription;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectEditDescription;

[Binding]
public class ProjectEditDescriptionSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;
    private ProjectEditDescriptionPayload? _response;
    private readonly DateTimeServiceFake _dateTimeServiceFake;


    public ProjectEditDescriptionSteps(QueryExceptionsContext errorStepContext,
        CurrentUserProviderFake currentUserProviderFake,
        StepsArgs args, DateTimeServiceFake dateTimeServiceFake) : base(args)
    {
        _errorStepContext = errorStepContext;
        _currentUserProviderFake = currentUserProviderFake;
        _dateTimeServiceFake = dateTimeServiceFake;
    }


    [Given(@"существует проект '(.*)' с владельцем '(.*)' и с описанием '(.*)'")]
    public async Task GivenСуществуетПроектСНазваниемВладельцемИОписанием(string projectName, string ownerUserName,
        string description)
    {
        _currentUserProviderFake.LoginAs(ownerUserName);
        var audit = AuditInfoDocument.Create(_dateTimeServiceFake.UtcNow, _currentUserProviderFake.UserId);

        var project = GiveMe.Project().WithProjectName(projectName).WithOwnerUserId(_currentUserProviderFake.UserId)
            .WithDescription(description).WithAuditInfo(audit);
        await Db.Projects.InsertOneAsync(project);
    }

    [When(@"'(.*)' редактирует описание проекта '(.*)' на '(.*)'")]
    public async Task WhenПользовательРедактируетОписаниеПроекта(string username, string projectName,
        string projectDescription)
    {
        _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        var input = new ProjectEditDescriptionInput(project.Id, projectDescription);

        try
        {
            _response = await Mutation.ProjectEditDescription(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"описание проекта '(.*)' состоит из '(.*)'")]
    public async Task ThenОписаниеПроектаСостоитИз(string projectName, string description)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        project.Description.Should().Be(description);
    }

    [Then(@"пользователь получает ошибку, что '([^']*)'")]
    public Task ThenПользовательПолучаетОшибку(string errorMsg)
    {
        var validError = _errorStepContext.QueryExceptions.First().Errors.Any(x => x.Code == errorMsg);
        validError.Should().BeTrue();
        return Task.CompletedTask;
    }

    [Then(@"карточка проекта '(.*)' содержит описание '(.*)'")]
    public async Task ThenКарточкаПроектаСодержитОписание(string projectName, string description)
    {
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        project.Description.Should().Be(description);
    }
}