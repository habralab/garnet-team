using FluentAssertions;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.Api.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamParticipantsUsers_ProjectsGet;

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
        var userParticipants = new List<ProjectUserEntity>();
        for (var i = 0; i < participantCount; i++)
        {
            userParticipants.Add(new UserParticipant(
                Uuid.NewMongo(),
                $"User{i}",
                ""));
        }

        await Db.ProjectTeamsParticipants.UpdateOneAsync(
            _f.Eq(x => x.TeamName, teamName),
            _u.Set<UserParticipant[]>(x => x.UserParticipants, userParticipants.ToArray())
        );
    }

    [When(@"происходит получение списка команд-участников проекта '([^']*)'")]
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
}