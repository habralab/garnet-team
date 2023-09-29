using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests.Support;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectGet;

[Binding]
public class ProjectGetSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;

    public ProjectGetSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
    }

    [Given(@"существует пользователь '([^']*)'")]
    public Task GivenСуществуетПользователь(string username)
    {
        _currentUserProviderFake.RegisterUser(username, Uuid.NewMongo());
        return Task.CompletedTask;
    }

    [Given(@"существует проект '(.*)' с описанием '(.*)'")]
    public async Task ThenСуществуетПроектСНазваниемИОписанием(string projectName, string description)
    {
        var project = GiveMe.Project().WithProjectName(projectName).WithDescription(description);
        await Db.Projects.InsertOneAsync(project);
    }

    [When(@"пользователь '(.*)' просматривает карточку проекта '(.*)'")]
    public async Task WhenПользовательПросматриваетПроект(string username, string projectName)
    {

    }

    [Then(@"описание проекта состоит из '(.*)'")]
    public async Task ThenОписаниеПроектаСостоитИз(string description)
    {

    }
}