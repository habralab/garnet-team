using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
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

    public ProjectParticipantsGetRatingSteps(StepsArgs args) :
        base(args)
    {
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

    [Then(@"в системе существует задача с названием '([^']*)' и статусом '([^']*)'")]
    public async Task ThenВСистемеСуществуетЗадачаСоСтатусом(string taskName, string status)
    {
        var task = await Db.ProjectTasks.Find(x => x.Name == taskName).FirstOrDefaultAsync();
        task.Status.Should().Be(status);
    }
}