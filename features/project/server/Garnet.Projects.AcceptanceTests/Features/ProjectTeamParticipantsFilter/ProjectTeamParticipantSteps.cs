using FluentAssertions;
using Garnet.Projects.AcceptanceTests.Support;
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

    private ProjectTeamParticipantPayload? _response;

    public ProjectTeamParticipantSteps(StepsArgs args) : base(args)
    {
    }

    [Given(@"существует команда '([^']*)'")]
    public async Task GivenСуществуетКоманда(string teamName)
    {
        var team = GiveMe.ProjectTeamParticipant().WithTeamName(teamName);
        await Db.ProjectTeamsParticipants.InsertOneAsync(team);
    }

    [Given(@"команда '([^']*)' является участником проекта '([^']*)'")]
    public async Task GivenКомандаЯвляетсяУчастникомПроекта(string teamName, string projectName)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var team = await Db.ProjectTeamsParticipants.Find(x => x.TeamName == teamName).FirstAsync();

        await Db.ProjectTeamsParticipants.FindOneAndUpdateAsync(
            _f.Eq(x => x.TeamId, team.TeamId),
            _u.Set(o => o.ProjectId, project.Id)
        );
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
        _response!.projectTeamParticipant.Count().Should().Be(teamCount);
        return Task.CompletedTask;
    }
}