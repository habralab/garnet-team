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

    }

    [When(@"происходит получение списка команд-участников проекта '([^']*)'")]
    public async Task WhenПроисходитПолученияСпискаКомандУчастниковПроекта(string projectName)
    {

    }

    [Then(@"количество команд в списке равно '([^']*)'")]
    public async Task ThenКоличествоКомандВСписке(int teamCount)
    {

    }
}