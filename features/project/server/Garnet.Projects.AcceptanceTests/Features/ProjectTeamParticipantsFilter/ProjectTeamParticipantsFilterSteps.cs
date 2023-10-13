using FluentAssertions;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamParticipantsFilter;

[Binding]
public class ProjectTeamParticipantsFilterSteps : BaseSteps
{
    private readonly FilterDefinitionBuilder<ProjectTeamParticipantDocument> _f =
        Builders<ProjectTeamParticipantDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTeamParticipantDocument> _u =
        Builders<ProjectTeamParticipantDocument>.Update;

    private ProjectTeamParticipantPayload? _response;

    public ProjectTeamParticipantsFilterSteps(StepsArgs args) : base(args)
    {
    }

    [Given(@"существует команда '([^']*)'")]
    public async Task GivenСуществуетКоманда(string teamName)
    {
        var team = GiveMe.ProjectTeam().WithTeamName(teamName);
        await Db.ProjectTeams.InsertOneAsync(team);
    }

    [Given(@"команда '([^']*)' является участником проекта '([^']*)'")]
    public async Task GivenКомандаЯвляетсяУчастникомПроекта(string teamName, string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var team = await Db.ProjectTeams.Find(x => x.TeamName == teamName).FirstAsync();

        var teamParticipant = GiveMe.ProjectTeamParticipant().WithTeamId(team.Id).WithTeamName(team.TeamName)
            .WithProjectId(project.Id);
        await Db.ProjectTeamsParticipants.InsertOneAsync(teamParticipant);
    }

    [When(@"происходит получение списка команд-участников проекта '([^']*)'")]
    public async Task WhenПроисходитПолученияСпискаКомандУчастниковПроекта(string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        _response = await Query.ProjectTeamParticipantsFilter(CancellationToken.None,
            new ProjectTeamParticipantInput(project.Id));
    }

    [Then(@"количество команд в списке равно '([^']*)'")]
    public Task ThenКоличествоКомандВСписке(int teamCount)
    {
        _response!.ProjectTeamParticipant.Count().Should().Be(teamCount);
        return Task.CompletedTask;
    }
}