using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.MongoDb;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectCreate;

[Binding]
public class ProjectCreateSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;

    public ProjectCreateSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
    }
    
    [Given(@"существует пользователь '([^']*)'")]
    public async Task GivenСуществуетПользователь(string username)
    {
        
    }

    [When(@"пользователь '(.*)' создает проект '(.*)'")]
    public async Task WhenПользовательСоздаетПроект(string username, string projectName)
    {
        
    }

    [Then(@"в системе существует проект '(.*)'")]
    public async Task ThenСуществуетПроектСНазванием(string projectName)
    {

    }

    [Then(@"пользователь '(.*)' является владельцем проекта '(.*)'")]
    public async Task ThenПользовательЯвляетсяВладельцемПроекта(string username, string projectName)
    {

    }

}