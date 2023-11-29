using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Project.AcceptanceTests.FakeServices.NotificationFake;
using Garnet.Projects.AcceptanceTests.FakeServices.NotificationFake;
using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.Api.ProjectTeamJoinRequestDecide;
using Garnet.Projects.Infrastructure.MongoDb;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamJoinRequestDecide;

[Binding]
public class ProjectTeamJoinRequestDecideSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private readonly QueryExceptionsContext _queryExceptionsContext;
    private readonly SendNotificationCommandMessageFakeConsumer _sendNotificationCommandMessageFakeConsumer;
    private readonly DeleteNotificationCommandMessageFakeConsumer _deleteNotificationCommandMessageFakeConsumer;


    public ProjectTeamJoinRequestDecideSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        SendNotificationCommandMessageFakeConsumer sendNotificationCommandMessageFakeConsumer,
        DeleteNotificationCommandMessageFakeConsumer deleteNotificationCommandMessageFakeConsumer,
        QueryExceptionsContext queryExceptionsContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _queryExceptionsContext = queryExceptionsContext;
        _deleteNotificationCommandMessageFakeConsumer = deleteNotificationCommandMessageFakeConsumer;
        _sendNotificationCommandMessageFakeConsumer = sendNotificationCommandMessageFakeConsumer;
    }

    private async Task<ProjectTeamJoinRequestDecideInput> SetJoinRequestDecision(string projectName, string teamName,
        bool isApproved)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var userJoinRequest = await Db.ProjectTeamJoinRequests
            .Find(x => x.ProjectId == project.Id && x.TeamName == teamName).FirstAsync();

        return new ProjectTeamJoinRequestDecideInput(userJoinRequest.Id, isApproved);
    }


    [When(@"пользователь '([^']*)' принимает заявку на участие от команды '([^']*)' в проект '([^']*)'")]
    public async Task WhenПользовательПринимаетЗаявкуНаВступлениеВПроект(string username, string teamName,
        string projectName)
    {
        var input = await SetJoinRequestDecision(projectName, teamName, true);
        _currentUserProviderFake.LoginAs(username);

        try
        {
            await Mutation.ProjectTeamJoinRequestDecide(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _queryExceptionsContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"команда '([^']*)' является участником проекта '([^']*)'")]
    public async Task ThenКомандаЯвляетсяУчастникомПроекта(string teamName, string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var teamParticipant = await Db.ProjectTeamsParticipants
            .Find(x => x.TeamName == teamName & x.ProjectId == project.Id).FirstAsync();
        teamParticipant.Should().NotBeNull();
    }

    [Then(@"количество заявок на вступление в проект '([^']*)' равно '([^']*)'")]
    public async Task ThenКоличествоЗаявокНаВступлениеВПроектРавно(string projectName, int teamJoinRequestsCount)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var teamJoinRequests = await Db.ProjectTeamJoinRequests.Find(x => x.ProjectId == project.Id).ToListAsync();
        teamJoinRequests.Count().Should().Be(teamJoinRequestsCount);
    }

    [When(@"пользователь '([^']*)' отклоняет заявку на участие от команды '([^']*)' в проект '([^']*)'")]
    public async Task WhenПользовательОтклоняетЗаявкуНаВступлениеВПроект(string username, string teamName,
        string projectName)
    {
        var input = await SetJoinRequestDecision(projectName, teamName, false);
        _currentUserProviderFake.LoginAs(username);

        try
        {
            await Mutation.ProjectTeamJoinRequestDecide(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _queryExceptionsContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"команда '([^']*)' не является участником проекта '([^']*)'")]
    public async Task ThenКомандаНеЯвляетсяУчастникомПроекта(string teamName, string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var teamParticipant = await Db.ProjectTeamsParticipants
            .Find(x => x.TeamName == teamName & x.ProjectId == project.Id).FirstOrDefaultAsync();
        teamParticipant.Should().BeNull();
    }

    [Then(@"для пользователя '(.*)' нет уведомлений типа '(.*)'")]
    public async Task ThenДляПользователяНетУведомленийТипа(string username, string eventType)
    {
        var user = await Db.ProjectUsers.Find(x => x.UserName == username).FirstAsync();
        var message = _deleteNotificationCommandMessageFakeConsumer.DeletedNotifications
           .First(x => x.UserId == user.Id);
        message.Type.Should().Be(eventType);
    }

    [Then(@"для пользователя '(.*)' нет уведомлений со связанной сущностью командой '(.*)'")]
    public async Task ThenДляПользователяНетУведомленийСоСвязаннойСущностьюКомандой(string username, string teamName)
    {
        var user = await Db.ProjectUsers.Find(x => x.UserName == username).FirstAsync();
        var team = await Db.ProjectTeams.Find(x => x.TeamName == teamName).FirstAsync();
        var requestedForDeleteNotice = _deleteNotificationCommandMessageFakeConsumer.DeletedNotifications
           .First(x => x.UserId == user.Id);
        var notice = _sendNotificationCommandMessageFakeConsumer.Notifications
                .First(x => x.UserId == user.Id && x.QuotedEntities.Any(y => y.Id == team.Id));

        requestedForDeleteNotice.LinkedEntityId.Should().Be(notice.LinkedEntityId);
    }
}