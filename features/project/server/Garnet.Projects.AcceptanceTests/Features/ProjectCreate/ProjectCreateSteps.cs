using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectCreate;

[Binding]
public class ProjectCreateSteps : BaseSteps
{
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