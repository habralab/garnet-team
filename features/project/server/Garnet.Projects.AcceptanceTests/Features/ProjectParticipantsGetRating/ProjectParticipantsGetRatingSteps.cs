using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.AcceptanceTests.FakeServices;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Application.ProjectTask.Args;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectParticipantsGetRating;

[Binding]
public class ProjectParticipantsGetRatingSteps : BaseSteps
{
    private readonly FilterDefinitionBuilder<ProjectTaskDocument> _f =
        Builders<ProjectTaskDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTaskDocument> _u =
        Builders<ProjectTaskDocument>.Update;

    private readonly ProjectTaskClosedEventFakeConsumer _taskClosedFake;
    private readonly CurrentUserProviderFake _currentUserProviderFake;

    public ProjectParticipantsGetRatingSteps(StepsArgs args, ProjectTaskClosedEventFakeConsumer taskClosedFake,
        CurrentUserProviderFake currentUserProviderFake) :
        base(args)
    {
        _taskClosedFake = taskClosedFake;
        _currentUserProviderFake = currentUserProviderFake;
    }

    [Given(@"задача '([^']*)' повторно открыта")]
    public async Task GivenЗадачаПовторноОткрыта(string taskName)
    {
        await Db.ProjectTasks.UpdateOneAsync(
            _f.Eq(x => x.Name, taskName),
            _u.Set(x => x.Status, ProjectTaskStatuses.Open)
                .Set(x => x.Reopened, true));
    }

    [Given(@"существует задача '([^']*)' в проекте '([^']*)' с тегами '([^']*)'")]
    public async Task GivenСуществуетЗадачаВПроектеСТегами(string taskName, string projectName, string tags)
    {
        var tagList = tags.Split(", ");
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var task = GiveMe.ProjectTask().WithName(taskName).WithProjectId(project.Id).WithTags(tagList).Build();
        await Db.ProjectTasks.InsertOneAsync(task);
    }

    [Given(@"пользователь '(.*)' является исполнителем задачи '(.*)'")]
    public async Task GivenПользовательЯвляетсяИсполнителемЗадачи(string username, string taskName)
    {
        var user = await Db.ProjectUsers.Find(x => x.UserName == username).FirstAsync();
        await Db.ProjectTasks.UpdateOneAsync(
            _f.Eq(x => x.Name, taskName),
            _u.AddToSet(x => x.UserExecutorIds, user.Id));
    }

    [Then(@"в системе существует задача с названием '([^']*)' и статусом '([^']*)'")]
    public async Task ThenВСистемеСуществуетЗадачаСоСтатусом(string taskName, string status)
    {
        var task = await Db.ProjectTasks.Find(x => x.Name == taskName).FirstOrDefaultAsync();
        task.Status.Should().Be(status);
    }

    [Then(
        @"у пользователя '([^']*)' общий рейтинг равен '([^']*)' а рейтинг каждого из навыков '([^']*)' равен '([^']*)'")]
    public void ThenУПользователяОбщийРейтингИРейтингНавыков(string username, float totalScore, string tags,
        float skillScore)
    {
        var tagList = tags.Split(", ");

        _taskClosedFake.GetMessage().RatingCalculation!.UserExecutorIds
            .Contains(_currentUserProviderFake.GetUserIdByUsername(username)).Should().BeTrue();

        _taskClosedFake.GetMessage().RatingCalculation!.UserTotalScore.Should().Be(totalScore);

        var skills = _taskClosedFake.GetMessage().RatingCalculation!.SkillScorePerUser;
        foreach (var tag in tagList)
        {
            skills[tag].Should().Be(skillScore);
        }
    }

    [Then(@"у команды '(.*)' общий рейтинг равен '(.*)'")]
    public async void ThenУКомандыОбщийРейтинг(string teamName, float totalScore)
    {
        var team = await Db.ProjectTeams.Find(x => x.TeamName == teamName).FirstAsync();
        _taskClosedFake.GetMessage().RatingCalculation!.TeamsTotalScore[team.Id].Should().Be(totalScore);
    }

    [Then(@"у пользователя '([^']*)' общий рейтинг равен '([^']*)'")]
    public void ThenУПользователяОбщийРейтинг(string username, float totalScore)
    {
        _taskClosedFake.GetMessage().RatingCalculation!.ProjectOwnerId.Should()
            .Be(_currentUserProviderFake.GetUserIdByUsername(username));
        _taskClosedFake.GetMessage().RatingCalculation!.ProjectOwnerTotalScore.Should().Be(totalScore);
    }
}