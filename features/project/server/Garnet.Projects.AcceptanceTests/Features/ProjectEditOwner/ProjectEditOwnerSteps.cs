using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectEdit;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectEditOwner;

[Binding]
public class ProjectEditOwnerSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    public ProjectEditOwnerSteps(QueryExceptionsContext errorStepContext, CurrentUserProviderFake currentUserProviderFake,
        StepsArgs args) : base(args)
    {
        _errorStepContext = errorStepContext;
        _currentUserProviderFake = currentUserProviderFake;
    }

    [When(@"'([^']*)' изменяет владельца проекта '([^']*)' на пользователя '([^']*)'")]
    public async Task WhenПользовательМеняетВладельцаПроекта(string userName, string projectName,
        string newOwnerName)
    {

    }

    [Then(@"владельцем проекта '([^']*)' является  '([^']*)'")]
    public async Task ThenВладельцемПроектаСтановится(string projectName, string newOwnerName)
    {

    }
}