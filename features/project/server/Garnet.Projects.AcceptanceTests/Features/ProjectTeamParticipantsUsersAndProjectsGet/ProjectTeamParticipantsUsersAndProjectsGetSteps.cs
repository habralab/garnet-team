using FluentAssertions;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectUser;
using Garnet.Projects.Infrastructure.Api.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamParticipantsUsersAndProjectsGet;

[Binding]
public class ProjectTeamParticipantsUsersAndProjectsGetSteps : BaseSteps
{
    private readonly FilterDefinitionBuilder<ProjectTeamParticipantDocument> _f =
        Builders<ProjectTeamParticipantDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTeamParticipantDocument> _u =
        Builders<ProjectTeamParticipantDocument>.Update;

    private ProjectTeamParticipantPayload? _response;

    public ProjectTeamParticipantsUsersAndProjectsGetSteps(StepsArgs args) : base(args)
    {
    }

    [Given(@"в команде '([^']*)' количество участников равно '([^']*)'")]
    public async Task GivenВКомандеКоличествоУчастниковРавно(string teamName, int participantCount)
    {
        var userParticipants = new List<ProjectUserDocument>();
        for (var i = 0; i < participantCount; i++)
        {
            var user = GiveMe.ProjectUser().WithUserName($"User{i}");
            userParticipants.Add(user);
        }

        await Db.ProjectTeamsParticipants.UpdateManyAsync(
            _f.Eq(x => x.TeamName, teamName),
            _u.Set<ProjectUserDocument[]>(x => x.UserParticipants, userParticipants.ToArray())
        );
    }

    [Given(@"в команде '([^']*)' количество проектов равно '([^']*)'")]
    public async Task GivenВКомандеКоличествоПроектовРавно(string teamName, int projectsCount)
    {
        var projectList = new List<ProjectDocument>();
        for (var i = 0; i < projectsCount; i++)
        {
            var project = GiveMe.Project().WithProjectName($"Project{i}");
            projectList.Add(project);
        }

        await Db.ProjectTeamsParticipants.UpdateManyAsync(
            _f.Eq(x => x.TeamName, teamName),
            _u.Set<ProjectDocument[]>(x => x.Projects, projectList.ToArray())
        );
    }

    [When(@"происходит получение списка команд участников проекта '([^']*)'")]
    public async Task WhenПроисходитПолученияСпискаКомандУчастниковПроекта(string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        _response = await Query.ProjectTeamParticipantsFilter(CancellationToken.None,
            new ProjectTeamParticipantInput(project.Id));
    }

    [Then(@"количество участников в первой команде списка равно '([^']*)'")]
    public Task ThenКоличествоУчастниковВПервойКомандеСпискаРавно(int userParticipantsCount)
    {
        _response!.ProjectTeamParticipant.First().UserParticipants.Count().Should().Be(userParticipantsCount);
        return Task.CompletedTask;
    }

    [Then(@"количество проектов в первой команде списка равно '([^']*)'")]
    public Task ThenКоличествоПроектовВПервойКомандеСпискаРавно(int projectsCount)
    {
        _response!.ProjectTeamParticipant.First().Projects.Count().Should().Be(projectsCount);
        return Task.CompletedTask;
    }
}