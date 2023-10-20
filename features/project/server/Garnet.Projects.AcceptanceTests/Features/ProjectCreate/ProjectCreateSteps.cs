using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.Api.ProjectCreate;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;
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
        var user = ProjectUserDocument.Create(Uuid.NewMongo(), username);
        await Db.ProjectUsers.InsertOneAsync(user);
        _currentUserProviderFake.RegisterUser(username, user.Id);
    }

    [When(@"пользователь '(.*)' создает проект '(.*)'")]
    public async Task WhenПользовательСоздаетПроект(string username, string projectName)
    {
        _currentUserProviderFake.LoginAs(username);
        var input = new ProjectCreateInput(projectName, string.Empty, null, Array.Empty<string>());
        await Mutation.ProjectCreate(CancellationToken.None, input);
    }

    [Then(@"в системе существует проект '(.*)'")]
    public async Task ThenСуществуетПроектСНазванием(string projectName)
    {
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        project.ProjectName.Should().Be(projectName);
    }

    [Then(@"пользователь '(.*)' является владельцем проекта '(.*)'")]
    public async Task ThenПользовательЯвляетсяВладельцемПроекта(string username, string projectName)
    {
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        _currentUserProviderFake.GetUserName(project.OwnerUserId).Should().Be(username);
    }
}