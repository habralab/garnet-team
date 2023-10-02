using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectGet;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectGet;

[Binding]
public class ProjectDeleteSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private ProjectPayload? _response;

    public ProjectDeleteSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
    }


    [Given(@"существует проект '(.*)' с владельцем '(.*)'")]
    public async Task ThenСуществуетПроектСНазваниемИВладельцем(string projectName, string ownerUserId)
    {

    }

    [When(@"'(.*)' удаляет проект '(.*)'")]
    public async Task WhenПользовательУдаляетПроект(string username, string projectName)
    {

    }

    [Then(@"проекта '(.*)' в системе не существует")]
    public async Task ThenПроектаНеСуществует(string projectName)
    {

    }

    [Then(@"пользователь получает ошибку, что '(.*)'")]
    public async Task ThenПользовательПолучаетОшибку(string errorMsg)
    {

    }

    [Then(@"в системе присутствует проект '(.*)'")]
    public async Task ThenПроектСуществуетВСистеме(string projectName)
    {

    }
}