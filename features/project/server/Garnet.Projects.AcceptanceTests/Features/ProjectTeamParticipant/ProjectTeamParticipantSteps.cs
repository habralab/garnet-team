using FluentAssertions;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamParticipant;

[Binding]
public class ProjectTeamParticipantSteps : BaseSteps
{
    private readonly FilterDefinitionBuilder<ProjectTeamParticipantDocument> _f =
        Builders<ProjectTeamParticipantDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTeamParticipantDocument> _u =
        Builders<ProjectTeamParticipantDocument>.Update;

    private readonly ProjectTeamParticipantFake _teamParticipantFake;
    private ProjectTeamParticipantPayload[]? _response;

    public ProjectTeamParticipantSteps(StepsArgs args, ProjectTeamParticipantFake teamParticipantFake) : base(args)
    {
        _teamParticipantFake = teamParticipantFake;
    }

    [Given(@"существует команда '([^']*)'")]
    public async Task GivenСуществуетКоманда(string teamName)
    {
        var teamId = _teamParticipantFake.CreateTeam(teamName, Uuid.NewMongo());
        var team = ProjectTeamParticipantDocument.Create(Uuid.NewMongo(), teamId, Uuid.NewMongo());
        await Db.ProjectTeamsParticipants.InsertOneAsync(team);
    }

    [Given(@"команда '([^']*)' является участником проекта '([^']*)'")]
    public async Task GivenКомандаЯвляетсяУчастникомПроекта(string teamName, string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var teamId = _teamParticipantFake.GetTeamIdByTeamName(teamName);
        await Db.ProjectTeamsParticipants.FindOneAndUpdateAsync(
            _f.Eq(x => x.TeamId, teamId),
            _u.Set(o => o.ProjectId, project.Id)
        );
    }

    [When(@"происходит получение списка команд-участников проекта '([^']*)'")]
    public async Task WhenПроисходитПолученияСпискаКомандУчастниковПроекта(string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        _response = await Query.ProjectTeamParticipantGet(CancellationToken.None,
            new ProjectTeamParticipantInput(project.Id));
    }

    [Then(@"количество команд в списке равно '([^']*)'")]
    public Task ThenКоличествоКомандВСписке(int teamCount)
    {
        _response!.Count().Should().Be(teamCount);
        return Task.CompletedTask;
    }
}