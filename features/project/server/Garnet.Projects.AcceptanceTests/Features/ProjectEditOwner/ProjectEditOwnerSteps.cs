using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectEditOwner;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectEditOwner;

[Binding]
public class ProjectEditOwnerSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;
    private ProjectEditOwnerPayload? _response;

    public ProjectEditOwnerSteps(QueryExceptionsContext errorStepContext,
        CurrentUserProviderFake currentUserProviderFake,
        StepsArgs args) : base(args)
    {
        _errorStepContext = errorStepContext;
        _currentUserProviderFake = currentUserProviderFake;
    }

    [When(@"'([^']*)' изменяет владельца проекта '([^']*)' на пользователя '([^']*)'")]
    public async Task WhenПользовательМеняетВладельцаПроекта(string userName, string projectName,
        string newOwnerName)
    {
        _currentUserProviderFake.LoginAs(userName);
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        var input = new ProjectEditOwnerInput(project.Id, _currentUserProviderFake.GetUserIdByUsername(newOwnerName));

        try
        {
            _response = await Mutation.ProjectEditOwner(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"владельцем проекта '([^']*)' является  '([^']*)'")]
    public async Task ThenВладельцемПроектаСтановится(string projectName, string newOwnerName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        _currentUserProviderFake.GetUserName(project.OwnerUserId).Should().Be(newOwnerName);
    }

    [Then(@"для пользователя '(.*)' существует уведомление типа '(.*)'")]
    public Task ThenДляПользователяСуществуетУведомлениеТипа(string маша, string projectEditOwner)
    {
        return Task.CompletedTask;
    }

    [Then(@"в последнем уведомлении для пользователя '(.*)' связанной сущностью является проект '(.*)'")]
    public Task ThenВПоследнемУведомленииДляПользователяСвязаннойСущностьюЯвляетсяПроект(string маша0, string dummy)
    {
        return Task.CompletedTask;
    }
}