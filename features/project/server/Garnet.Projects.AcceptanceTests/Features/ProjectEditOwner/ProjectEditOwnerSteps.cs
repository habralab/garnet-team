using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Project.AcceptanceTests.FakeServices.NotificationFake;
using Garnet.Projects.Infrastructure.Api.ProjectEditOwner;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectEditOwner;

[Binding]
public class ProjectEditOwnerSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private readonly QueryExceptionsContext _errorStepContext;
    private readonly SendNotificationCommandMessageFakeConsumer _sendNotificationCommandMessageFakeConsumer;

    public ProjectEditOwnerSteps(QueryExceptionsContext errorStepContext,
        CurrentUserProviderFake currentUserProviderFake,
        SendNotificationCommandMessageFakeConsumer sendNotificationCommandMessageFakeConsumer,
        StepsArgs args) : base(args)
    {
        _errorStepContext = errorStepContext;
        _sendNotificationCommandMessageFakeConsumer = sendNotificationCommandMessageFakeConsumer;
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
            await Mutation.ProjectEditOwner(CancellationToken.None, input);
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
    public async Task ThenДляПользователяСуществуетУведомлениеТипа(string username, string eventType)
    {
        var user = await Db.ProjectUsers.Find(x => x.UserName == username).FirstAsync();
        var notification = _sendNotificationCommandMessageFakeConsumer.Notifications
            .First(x => x.UserId == user.Id);
        notification.Type.Should().Be(eventType);
    }

    [Then(@"в последнем уведомлении для пользователя '(.*)' связанной сущностью является проект '(.*)'")]
    public async Task ThenВПоследнемУведомленииДляПользователяСвязаннойСущностьюЯвляетсяПроект(string username, string projectName)
    {
        var user = await Db.ProjectUsers.Find(x => x.UserName == username).FirstAsync();
        var notification = _sendNotificationCommandMessageFakeConsumer.Notifications
            .Last(x => x.UserId == user.Id);
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        notification.QuotedEntities.Should().Contain(x=> x.Id == project.Id);
    }
}