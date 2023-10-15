using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectEditName;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectEditName;

[Binding]
public class ProjectEditNameSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private readonly QueryExceptionsContext _errorStepContext;

    public ProjectEditNameSteps(
        QueryExceptionsContext errorStepContext,
        CurrentUserProviderFake currentUserProviderFake,
        StepsArgs args) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [When("'(.*)' редактирует название проекта '(.*)' на '(.*)'")]
    public async Task WhenРедактируетНазваниеПроекта(string username, string projectName, string newName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();

        _currentUserProviderFake.LoginAs(username);
        var input = new ProjectEditNameInput(project.Id, newName);

        try
        {
            await Mutation.ProjectEditName(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"в списке проектов пользователя '(.*)' есть проект '(.*)'")]
    public async Task ThenВСпискеПроектовПользователяЕстьПроект(string username, string projectName)
    {
        var projectList = await Db.Projects.Find(x =>
            x.OwnerUserId == _currentUserProviderFake.GetUserIdByUsername(username)
            ).ToListAsync();

        projectList.Any(x => x.ProjectName == projectName).Should().BeTrue();    }
}